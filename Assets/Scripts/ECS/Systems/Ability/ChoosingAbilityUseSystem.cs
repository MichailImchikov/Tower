using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class ChoosingAbilityUseSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<ChoosingAbilityUseEvent>> _filter;
        readonly EcsPoolInject<ChoosingAbilityUseEvent> _choosingAbilityPool;
        readonly EcsPoolInject<AbilityToUseComponent> _abilityToUsePool;
        readonly EcsPoolInject<AbilityContainer> _abilityContainerPool;
        readonly EcsWorldInject _world;
        public void Run (IEcsSystems systems) {
            foreach(var entity in _filter.Value)
            {
                ref var choosingAbility = ref _choosingAbilityPool.Value.Get(entity);
                if (!choosingAbility.abilityEntity.Unpack(_world.Value, out int abilityEntity)) continue;
                if (!choosingAbility.ownerAbility.Unpack(_world.Value, out int ownerAbility)) continue;
                ref var abilityContainerComp =  ref _abilityContainerPool.Value.Get(ownerAbility);
                abilityContainerComp.RemoveAbilities(_world.Value);
                _abilityToUsePool.Value.Add(abilityEntity);
            }
        }
    }
}