using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class CheckDirectionAttackSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<CheckDirectionAttackEvent,AttackZoneComponent,AttackAreaComponent>> _filter;
        readonly EcsPoolInject<AttackAreaComponent> _attackAreaPool;
        readonly EcsPoolInject<AttackZoneComponent> _attackZoneComponent;
        readonly EcsPoolInject<TurnAttackZoneEvent> _turnAttackPool;
        public void Run (IEcsSystems systems) {
            foreach(var entity in _filter.Value)
            {
                ref var attackZoneComponent = ref _attackZoneComponent.Value.Get(entity);
                ref var attackAreaComp = ref _attackAreaPool.Value.Get(entity);
                var permittedDirection = attackAreaComp.AttackArea[attackZoneComponent.Center];
                if (attackZoneComponent.Direction == permittedDirection || permittedDirection == 5) continue;
                _turnAttackPool.Value.Add(entity).NewDirection = permittedDirection;
            }
        }
    }
}