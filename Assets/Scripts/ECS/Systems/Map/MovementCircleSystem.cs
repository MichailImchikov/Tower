using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class MovementCircleSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<PlayerComponent, TransformComponent>> _filterPlayer;
        readonly EcsPoolInject<TransformComponent> _transformPool;
        readonly EcsPoolInject<CircleTransformComponent> _transformCirclePool;
        readonly EcsFilterInject<Inc<CircleTransformComponent>> _filterMap;

        public void Run (IEcsSystems systems) {
            
            foreach (var entity in _filterMap.Value)
            {
                ref var transformComp=ref _transformCirclePool.Value.Get(entity);
                transformComp.TransformCursor.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var worldPositionClick = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transformComp.TransformCursor.position = GameState.Instance.GetNewPoint(worldPositionClick).PointToWorld;
                foreach (var entityPlayer in _filterPlayer.Value)
                {
                    ref var transformPlayer = ref _transformPool.Value.Get(entityPlayer);
                    transformComp.TransformForPlayer.position = transformPlayer.Transform.position;
                }
            }
            
        }
    }
}