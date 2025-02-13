using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Text.RegularExpressions;
using Unity.Mathematics;
using UnityEngine;

namespace Client {
    sealed class ScrollCameraSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<ScrollMouseWheelEvent>> _filter;
        readonly EcsPoolInject<ScrollMouseWheelEvent> _scrollMousePool;
        public void Run(IEcsSystems systems)
        {
            foreach(var entity in _filter.Value)
            {
                ref var scrollMouseEvent = ref _scrollMousePool.Value.Get(entity);
                var newSizeCamera = Mathf.Clamp(Camera.main.orthographicSize - scrollMouseEvent.ScrollSize, 2, 10);
                Camera.main.orthographicSize = newSizeCamera;
            }
        }
    }
}