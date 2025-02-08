using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class DragAndDropSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<MouseClampComponent,MousePositionComponent>, Exc<DragAndDropEvent>> _filter;
        readonly EcsPoolInject<MousePositionComponent> _mousePositionPool;
        readonly EcsPoolInject<DragAndDropEvent> _dragAndDropPool;
        public void Run (IEcsSystems systems) {
            foreach(var entity in _filter.Value)
            {
                ref var mousePositionComp = ref _mousePositionPool.Value.Get(entity);
                var currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (Vector3.Distance(mousePositionComp.position, currentMousePosition) < 0.01f) continue;
                ref var dragAndDropEvent = ref _dragAndDropPool.Value.Add(entity);
                dragAndDropEvent.Diraction = mousePositionComp.position - currentMousePosition;
            }
        }
    }
}