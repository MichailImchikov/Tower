using Client;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class DrawAreaWalkingSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<DrawAreaWalkingEvent, AreaWalkingComponent>> _filter;
        readonly EcsPoolInject<AreaWalkingComponent> _areaWalkingPool;
        readonly EcsPoolInject<MapAreaDrawerComponent> _mapAreaDrawerPool;
        readonly EcsWorldInject _world;
        public void Run (IEcsSystems systems) {
            foreach(var entity in _filter.Value)
            {
                ref var areaWalkingComp = ref _areaWalkingPool.Value.Get(entity);
                if (!GameState.Instance.MapEntity.Unpack(_world.Value, out int entityMap)) continue;
                ref var mapAreaDrawerComp = ref _mapAreaDrawerPool.Value.Get(entityMap);
                foreach(var point in areaWalkingComp.areaWalking)
                {
                    mapAreaDrawerComp.tilemap.SetTile(point.Key.PointToMap, mapAreaDrawerComp.TileWay);
                }
            }
        }
    }
}
