using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class InitAbilitySystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<InitAbilityEvent>, Exc<AbilityComponent>> _filter;
        readonly EcsPoolInject<InitAbilityEvent> _initAbilityPool;
        readonly EcsPoolInject<AbilityComponent> _abilityPool;
        public void Run (IEcsSystems systems) {
            foreach(var entity in _filter.Value)
            {
                ref var initAbility = ref _initAbilityPool.Value.Get(entity);
                ref var abilityComp = ref _abilityPool.Value.Add(entity);
                abilityComp.ability = initAbility.ability;
                abilityComp.packedEntityOwner = initAbility.packedEntityOwner;
                abilityComp.IsOn=false;
                // todo event on DrawAreaAttack;
            }
        }
    }
}