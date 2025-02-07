using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class MovementCircleSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<PlayerComponent, AreaWalkingComponent>> _filterPlayer;
        readonly EcsPoolInject<CircleTransformComponent> _transformPool;
        readonly EcsFilterInject<Inc<CircleTransformComponent>> _filterMap;

        public void Run (IEcsSystems systems) {
            
            foreach (var entity in _filterMap.Value)
            {
                ref var transformComp=ref _transformPool.Value.Get(entity);
                transformComp.Transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var worldPositionClick = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transformComp.Transform.position = GameState.Instance.GetNewPoint(worldPositionClick).PointToWorld;
            }
        }
    }
}