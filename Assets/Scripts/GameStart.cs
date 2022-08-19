using Systems;
using LeopotamGroup.Ecs;
using UnityEngine;
namespace Appsu.Test 
{
    public class GameStart : MonoBehaviour
    {
        public float PlayerSpeed;
        SpawnSystem spawnSystem;
        EcsWorld _world;
        EcsSystems _systems;

        // Start is called before the first frame update
        void OnEnable()
        {
            _world = new EcsWorld();
            spawnSystem = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnSystem>();
            spawnSystem.gameObject.SetActive(true);
    #if UNITY_EDITOR
            LeopotamGroup.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
    #endif     
            _systems = new EcsSystems(_world)
                .Add(new UserInputSystem())
                .Add(new MovePlayerSystem());
            _systems.Initialize();
    #if UNITY_EDITOR
            LeopotamGroup.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
    #endif
        }

        private void Update()
        {
            _systems.Run();
        }

        private void OnDisable()
        {
            _systems.Destroy();
            _world.Dispose();
        }

    }
}
