using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class TurnAttackZoneSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<AttackZoneComponent,TurnAttackZoneEvent>> _filter;
        readonly EcsPoolInject<AttackAreaComponent> _attackAreaPool;
        readonly EcsPoolInject<AttackZoneComponent> _attackZonePool;
        readonly EcsPoolInject<TurnAttackZoneEvent> _turnAttackZoneEvent;
        public void Run (IEcsSystems systems) {
            foreach(var entity in _filter.Value)
            {
                ref var attackZoneComp = ref _attackZonePool.Value.Get(entity);
                ref var turnZoneComp = ref _turnAttackZoneEvent.Value.Get(entity);
                if (turnZoneComp.NewDirection <= 0 || turnZoneComp.NewDirection >= 5) continue;
                attackZoneComp.TurnByDirection(turnZoneComp.NewDirection);
            }
        }
    }
}