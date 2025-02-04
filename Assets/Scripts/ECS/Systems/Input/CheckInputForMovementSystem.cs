using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class CheckInputForMovementSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<MouseClickEvent>> _filterClick;
        readonly EcsFilterInject<Inc<PlayerComponent, AreaWalkingComponent>> _filterPlayer;
        readonly EcsPoolInject<AreaWalkingComponent> _areaWalkingPool;
        readonly EcsPoolInject<MouseClickEvent> _mouseClickPool;
        readonly EcsPoolInject<CreateWayToPointEvent> _createWayToPointEvent;
        readonly EcsPoolInject<ClearMapDrawerEvent> _clearMapDrawerPool;
        readonly EcsWorldInject _world;
        public void Run (IEcsSystems systems) {
            foreach(var entityClick in _filterClick.Value)
            {
                ref var mouseClickComp = ref _mouseClickPool.Value.Get(entityClick);
                //int entityPlayer = _filterPlayer.Value.GetRawEntities()[0];
                foreach(var entityPlayer in _filterPlayer.Value)
                {
                    ref var areWalkingComp = ref _areaWalkingPool.Value.Get(entityPlayer);
                    var pointInMapClick = mouseClickComp.positionClick;
                    if (areWalkingComp.areaWalking.ContainsKey(pointInMapClick))
                    {
                        ref var createWayToPointComp = ref _createWayToPointEvent.Value.Add(entityPlayer);
                        createWayToPointComp.pointMap = pointInMapClick;
                        _clearMapDrawerPool.Value.Add(_world.Value.NewEntity());
                        _mouseClickPool.Value.Del(entityClick);
                    }
                }
            }
        }
    }
}