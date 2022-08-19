using Components;
using LeopotamGroup.Ecs;
using UnityEngine;

namespace Systems
{
	[EcsInject]
	sealed class UserInputSystem : IEcsRunSystem
	{
		EcsWorld _world = null;
		Vector3 prevPos = Vector3.zero; 
		public void Run()
		{
			Vector3 x = Vector3.zero;
			if (Input.touchCount > 0)
			{
				Camera cam = Camera.main;
				x = cam.ScreenToWorldPoint(Input.touches[0].position);
				prevPos = x;
			}
			else x = prevPos;
			Direction direction = new Direction();

			direction.Dist = x;
			//Debug.Log("Dist = "+ direction.Dist);
			var inputEvent = _world.CreateEntityWith<UserInputEvent>();
			inputEvent.MoveDirection = direction;
		} 
	}
}
