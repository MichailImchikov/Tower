using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class RequestInvokeSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<RequestInvokeEvent>> _filterPlayer;
        readonly EcsFilterInject<Inc<AbilityComponent>> _filterAbility;
        readonly EcsPoolInject<AbilityComponent> _abilityPool;
        readonly EcsPoolInject<AbilityPointsComponent> _abilityPointsPool;
        readonly EcsPoolInject<InvokeAbilityEvent> _invokeAbilityPool;
        readonly EcsWorldInject _world;
        public void Run (IEcsSystems systems) {
            foreach(var entity in _filterPlayer.Value)
            {
                ref var abilityPointsComp = ref _abilityPointsPool.Value.Get(entity);
                foreach(var ability in _filterAbility.Value)
                {
                    ref var abilityComp = ref _abilityPool.Value.Get(ability);
                    if (abilityPointsComp.CurrentValue >= abilityComp.ability.Cost) _invokeAbilityPool.Value.Add(ability);
                }
                

                
            }
        }
    }
}