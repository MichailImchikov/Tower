using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class ClearMapDrawerSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<ClearMapDrawerEvent>> _filter;
        readonly EcsPoolInject<MapAreaDrawerComponent> _mapAreaDrawerPool;
        readonly EcsPoolInject<ClearMapDrawerEvent> _clearMapDrawerPool;
        readonly EcsWorldInject _world;
        public void Run (IEcsSystems systems) {
            foreach (var entity in _filter.Value)
            {
                if (!GameState.Instance.MapEntity.Unpack(_world.Value, out int entityMap)) continue;
                ref var mapDrawer = ref _mapAreaDrawerPool.Value.Get(entityMap);
                mapDrawer.tilemap.ClearAllTiles();
                _clearMapDrawerPool.Value.Del(entity);
            }
        }
    }
}