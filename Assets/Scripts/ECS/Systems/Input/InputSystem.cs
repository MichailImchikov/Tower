using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class InputSystem : IEcsRunSystem
    {
        readonly EcsPoolInject<MouseClickEvent> _mouseClickPool;
        readonly EcsWorldInject _world;
        public void Run(IEcsSystems systems)
        {
            if(Input.GetMouseButtonUp(0))
            {
                ref var mouseClickComp = ref _mouseClickPool.Value.Add(_world.Value.NewEntity());
                var worldPositionClick = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseClickComp.positionClick = GameState.Instance.GetNewPoint(worldPositionClick); 
            }
        }
    }
}