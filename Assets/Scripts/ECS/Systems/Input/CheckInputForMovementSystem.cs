using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class CheckInputForMovementSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<MouseClickUpEvent,MousePositionComponent>> _filterClick;
        readonly EcsFilterInject<Inc<PlayerComponent, AreaWalkingComponent,MoveStateComponent>> _filterPlayer;
        readonly EcsPoolInject<AreaWalkingComponent> _areaWalkingPool;
        readonly EcsPoolInject<MousePositionComponent> _mousePositionPool;
        readonly EcsPoolInject<CreateWayToPointEvent> _createWayToPointEvent;
        readonly EcsPoolInject<ClearMapDrawerEvent> _clearMapDrawerPool;
        readonly EcsPoolInject<MouseClickUpEvent> _mouseClickUpPool;
        readonly EcsWorldInject _world;
        readonly EcsPoolInject<RequestAnimationEvent> _requestAnimationPool;
        public void Run (IEcsSystems systems) {
            foreach(var entityClick in _filterClick.Value)
            {
                ref var mousePositionComp = ref _mousePositionPool.Value.Get(entityClick);
                foreach(var entityPlayer in _filterPlayer.Value)
                {
                    ref var areWalkingComp = ref _areaWalkingPool.Value.Get(entityPlayer);
                    var pointInMapClick = mousePositionComp.pointMap;
                    if (areWalkingComp.areaWalking.ContainsKey(pointInMapClick))
                    {
                        ref var createWayToPointComp = ref _createWayToPointEvent.Value.Add(entityPlayer);
                        createWayToPointComp.pointMap = pointInMapClick;
                        ref var requestAnimationEvent = ref _requestAnimationPool.Value.Add(_world.Value.NewEntity());
                        requestAnimationEvent.entityPacked = _world.Value.PackEntity(entityPlayer);
                        requestAnimationEvent.State = AnimationState.Move;
                        _clearMapDrawerPool.Value.Add(_world.Value.NewEntity());
                        _mouseClickUpPool.Value.Del(entityClick);
                    }
                }
            }
        }
    }
}