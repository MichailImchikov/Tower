using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine.Rendering;

namespace Client {
    sealed class CheckInvokeAbilitySystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<AttackZoneComponent, AbilityToUseComponent>> _filter;
        readonly EcsFilterInject<Inc<MouseClickUpEvent, MousePositionComponent>> _filterInput;
        readonly EcsPoolInject<MousePositionComponent> _mousePositionPool;
        readonly EcsPoolInject<AttackZoneComponent> _attackZonePool;
        readonly EcsPoolInject<InvokeAbilityEvent> _invokeAbility;
        readonly EcsPoolInject<MouseClickUpEvent> _mouseClickUpPool;
        public void Run(IEcsSystems systems)
        {
            foreach(var entity in _filterInput.Value)
            {
                ref var mouseClick = ref _mousePositionPool.Value.Get(entity);
                foreach(var entityAbility in _filter.Value)
                {
                    ref var abilityAttackZone = ref _attackZonePool.Value.Get(entityAbility);
                    if (mouseClick.pointMap.PointToMap != abilityAttackZone.Click.PointToMap) continue;
                    _invokeAbility.Value.Add(entityAbility);
                    _mouseClickUpPool.Value.Del(entity);
                }
            }
        }
    }
}