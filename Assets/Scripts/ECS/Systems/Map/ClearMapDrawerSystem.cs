using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class ClearMapDrawerSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<ClearMapDrawerEvent>> _filter;
        readonly EcsFilterInject<Inc<MapAreaDrawerComponent>> _filterMap;
        readonly EcsPoolInject<MapAreaDrawerComponent> _mapAreaDrawerPool;
        readonly EcsPoolInject<ClearMapDrawerEvent> _clearMapDrawerPool;
        public void Run (IEcsSystems systems) {
            foreach (var entity in _filter.Value)
            {
                int entityMap = _filterMap.Value.GetRawEntities()[0];
                ref var mapDrawer = ref _mapAreaDrawerPool.Value.Get(entityMap);
                mapDrawer.tilemap.ClearAllTiles();
                _clearMapDrawerPool.Value.Del(entity);
            }
        }
    }
}