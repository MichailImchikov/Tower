using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class RelocateAttackZoneSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<AttackZoneComponent, InitAttackZoneEvent>> _filter;
        readonly EcsPoolInject<AttackZoneComponent> _attackZonePool;
        readonly EcsPoolInject<InitAttackZoneEvent> _initAttackZoneEvent;
        readonly EcsPoolInject<DrawAttackZoneEvent> _drawAttackZonePool;
        readonly EcsPoolInject<ClearMapDrawerEvent> _clearMapDrawerPool;
        readonly EcsWorldInject _world;
        public void Run (IEcsSystems systems) {
            foreach(var entity in _filter.Value)
            {
                ref var attackZoneComp = ref _attackZonePool.Value.Get(entity);
                ref var initAttackZone =  ref _initAttackZoneEvent.Value.Get(entity);
                attackZoneComp.NewCenter(initAttackZone.pointCenter.PointToMap);
                _drawAttackZonePool.Value.Add(entity);
                _clearMapDrawerPool.Value.Add(_world.Value.NewEntity());
            }
        }
    }
}