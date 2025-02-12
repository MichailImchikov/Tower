using Client;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class DrawAttackZoneSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<AttackZoneComponent>> _filter;
        readonly EcsPoolInject<AttackZoneComponent> _attackZonePool;
        readonly EcsFilterInject<Inc<MapAreaDrawerComponent>> _filterDrawer;
        readonly EcsPoolInject<MapAreaDrawerComponent> _mapAreaDrawerPool;
        public void Run (IEcsSystems systems) {
            foreach(var entity in _filter.Value)
            {
                var entityMap = _filterDrawer.Value.GetRawEntities()[0];
                ref var mapAreaDrawerComp = ref _mapAreaDrawerPool.Value.Get(entityMap);
                ref var AttackZonePoints = ref _attackZonePool.Value.Get(entity);
                foreach(var point in AttackZonePoints.pointAttack)
                {
                    mapAreaDrawerComp.tilemap.SetTile(point.PointToMap, mapAreaDrawerComp.TileWay);
                }
            }
        }
    }
}
