using Client;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class DrawAttackZoneSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<AttackZoneComponent>> _filter;
        readonly EcsPoolInject<AttackZoneComponent> _attackZonePool;
        readonly EcsPoolInject<MapAreaDrawerComponent> _mapAreaDrawerPool;
        readonly EcsPoolInject<AbilityComponent> _abilityPool;
        readonly EcsWorldInject _world;
        public void Run (IEcsSystems systems) {
            foreach(var entity in _filter.Value)
            {
                if (!GameState.Instance.MapEntity.Unpack(_world.Value, out int entityMap)) continue;
                ref var mapAreaDrawerComp = ref _mapAreaDrawerPool.Value.Get(entityMap);
                ref var AttackZonePoints = ref _attackZonePool.Value.Get(entity);
                ref var abilityComponent = ref _abilityPool.Value.Get(entity);
                foreach (var point in AttackZonePoints.pointAttack)
                {
                    mapAreaDrawerComp.tilemap.SetTile(point.PointToMap, abilityComponent.ability.TileView);
                }
            }
        }
    }
}
