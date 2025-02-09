using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class InputSystem : IEcsRunSystem, IEcsInitSystem
    {
        readonly EcsFilterInject<Inc<MousePositionComponent>> _filterInput;
        readonly EcsPoolInject<MouseClickUpEvent> _mouseClickUpPool;
        readonly EcsPoolInject<MouseClickDownEvent> _mouseClickDownPool;
        readonly EcsPoolInject<MousePositionComponent> _mousePositionComponent;
        readonly EcsPoolInject<ScrollMouseWheelEvent> _scrollMouseWheelPool;
        readonly EcsWorldInject _world;

        public void Init(IEcsSystems systems)
        {
            var entityInput = _world.Value.NewEntity();
            ref var mousePositionComp = ref _mousePositionComponent.Value.Add(entityInput);
            mousePositionComp.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        public void Run(IEcsSystems systems)
        {
            foreach(var entity in _filterInput.Value)
            {
                ref var mousePositionComp = ref _mousePositionComponent.Value.Get(entity);
                mousePositionComp.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                if (Input.GetMouseButtonUp(0))
                    _mouseClickUpPool.Value.Add(entity);
                if (Input.GetMouseButtonDown(0))
                    _mouseClickDownPool.Value.Add(entity);
                if (Input.mouseScrollDelta.y != 0)
                    _scrollMouseWheelPool.Value.Add(entity).ScrollSize = Input.mouseScrollDelta.y;
            }
        }
    }
}