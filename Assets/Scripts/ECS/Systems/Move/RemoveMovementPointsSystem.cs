using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class RemoveMovementPointsSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<RemoveMovementPointsEvent, MovePointsComponent>> _filter;
        readonly EcsPoolInject<MovePointsComponent> _movementPointPool;
        readonly EcsPoolInject<RemoveMovementPointsEvent> _removeMovementPointsPool;
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var movementPoints = ref _movementPointPool.Value.Get(entity);
                ref var removePoints = ref _removeMovementPointsPool.Value.Get(entity);
                movementPoints.CurrentValue -= removePoints.Value;
            }
            
        }
    }
}