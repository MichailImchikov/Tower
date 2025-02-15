using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class InvokeAbilitySystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<AbilityComponent, AttackZoneComponent, InvokeAbilityEvent>> _filter;
        readonly EcsPoolInject<AttackZoneComponent> _attackZonePool;
        readonly EcsPoolInject<RequestDamageEvent> _requestDamageEvent;
        readonly EcsPoolInject<AbilityComponent> _abilityPool;
        readonly EcsPoolInject<ClearMapDrawerEvent> _clearMapDrawerPool;
        readonly EcsPoolInject<AbilityPointsComponent> _abilityPointsPool;
        readonly EcsWorldInject _world;
        public void Run (IEcsSystems systems) {
            foreach(var entity in _filter.Value)
            {
                ref var attackZoneComp = ref _attackZonePool.Value.Get(entity);
                ref var abilityComp = ref _abilityPool.Value.Get(entity);
                ref var abilityPointsComp = ref _abilityPointsPool.Value.Get(abilityComp.packedEntityOwner.Id);
                foreach(var point in attackZoneComp.pointAttack)
                {
                    ref var requestDamageComp = ref _requestDamageEvent.Value.Add(_world.Value.NewEntity());
                    requestDamageComp.PointInMap = point;
                    requestDamageComp.Damage = abilityComp.ability.Damage;
                    
                }
                abilityPointsComp.CurrentValue-=abilityComp.ability.Cost;
                _attackZonePool.Value.Del(entity);
                _clearMapDrawerPool.Value.Add(_world.Value.NewEntity());
            }
        }
    }
}