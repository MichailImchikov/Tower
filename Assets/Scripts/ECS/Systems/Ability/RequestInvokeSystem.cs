using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEditor.Playables;

namespace Client {
    sealed class RequestInvokeSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<RequestInvokeEvent>> _filter;
        readonly EcsPoolInject<AbilityComponent> _abilityPool;
        readonly EcsPoolInject<AbilityPointsComponent> _abilityPointsPool;
        readonly EcsPoolInject<InvokeAbilityEvent> _invokeAbilityPool;
        public void Run (IEcsSystems systems) {
            foreach(var entity in _filter.Value)
            {
                ref var abilityComp = ref _abilityPool.Value.Get(entity);
                ref var abilityPointsComp = ref _abilityPointsPool.Value.Get(abilityComp.packedEntityOwner.Id);
                if (abilityPointsComp.CurrentValue >= abilityComp.ability.Cost)
                {
                    _invokeAbilityPool.Value.Add(entity);
                    abilityPointsComp.CurrentValue -= abilityComp.ability.Cost;
                }
            }
        }
    }
}