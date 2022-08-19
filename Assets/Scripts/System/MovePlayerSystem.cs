using Components;
using LeopotamGroup.Ecs;
using UnityEngine;

namespace Systems
{
    public struct Direction
    {
        Vector3 distanation;
        public Vector3 Dist { set { distanation = value; } get { return distanation; } }
    }

    [EcsInject]
    sealed class MovePlayerSystem : IEcsInitSystem, IEcsRunSystem
    {
        private const float PlayerSpeed = 0.2f;
        
        const string PlayerTag = "Player";

        EcsWorld _world = null;
        EcsFilter<Player> _playerFilter = null;
        EcsFilter<UserInputEvent> _userInputFilter = null;

        public void Initialize()
        {
            foreach (var unityObject in GameObject.FindGameObjectsWithTag(PlayerTag))
            {
                var tr = unityObject.transform;
                var player = _world.CreateEntityWith<Player>();
                player.Speed = PlayerSpeed;
                player.Transform = tr;
            }
        }

        public void Destroy()
        {
        }

        public void Run()
        {
            for (int i = 0; i < _userInputFilter.EntitiesCount; i++)
            {
                var direction = _userInputFilter.Components1[i].MoveDirection;
                if (direction.Dist != Vector3.zero) 
                {
                    for (int j = 0; j < _playerFilter.EntitiesCount; j++)
                    {
                        var player = _playerFilter.Components1[j];
                        float delta = Time.deltaTime * PlayerSpeed;

                        player.Transform.position = 
                            Vector3.Lerp(player.Transform.position , new Vector3(direction.Dist.x, 0, direction.Dist.z), delta)  ;

                        var magnitude = (player.Transform.position - new Vector3(direction.Dist.x, 0, direction.Dist.z)).magnitude;
                        
                        if (magnitude > 0.35f) UI.UIController.AddDistanse(0.01f);
                    } 
                }
                _world.RemoveEntity(_userInputFilter.Entities[i]);
            }
        }
    }
}