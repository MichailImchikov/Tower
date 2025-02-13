using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class MoveCameraSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<DragAndDropEvent>> _filter;
        readonly EcsPoolInject<DragAndDropEvent> _dragAndDropSystem;
        public void Run (IEcsSystems systems) {
            foreach(var entity in _filter.Value)
            {
                ref var dragAndDrop = ref _dragAndDropSystem.Value.Get(entity);
                Camera.main.transform.position += dragAndDrop.Diraction;
                _dragAndDropSystem.Value.Del(entity);
            }
        }
    }
}