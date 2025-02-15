using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine.Rendering;
using static UnityEngine.EventSystems.EventTrigger;

namespace Client {
    sealed class CheckInvokeAbilitySystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<PlayerComponent, AbilityToUseComponent, AttackStateComponent>> _filterPlayer;
        readonly EcsFilterInject<Inc<AttackZoneComponent, AbilityToUseComponent>> _filter;
        readonly EcsFilterInject<Inc<MouseClickUpEvent, MousePositionComponent>> _filterInput;
        readonly EcsPoolInject<MousePositionComponent> _mousePositionPool;
        readonly EcsPoolInject<AttackZoneComponent> _attackZonePool;
        readonly EcsPoolInject<MouseClickUpEvent> _mouseClickUpPool;
        readonly EcsPoolInject<AbilityToUseComponent> _abilityToUsePool;
        readonly EcsPoolInject<RequestInvokeEvent> _requestInvokePool;
        readonly EcsWorldInject _world;
        public void Run(IEcsSystems systems)
        {
            foreach (var entityPlayer in _filterPlayer.Value)
            {
                ref var abilityToUse = ref _abilityToUsePool.Value.Get(entityPlayer);
                if (!abilityToUse.entityAbility.Unpack(_world.Value, out int abilityEntity)) continue;
                if (!_attackZonePool.Value.Has(abilityEntity)) continue;
                ref var attackZoneComp = ref _attackZonePool.Value.Get(abilityEntity);
                foreach (var entityClick in _filterInput.Value)
                {
                    ref var mouseClick = ref _mousePositionPool.Value.Get(entityClick);
                    if (mouseClick.pointMap.PointToMap != attackZoneComp.Click.PointToMap) continue;
                    _requestInvokePool.Value.Add(abilityEntity);
                    _mouseClickUpPool.Value.Del(entityClick);
                }
            }
            //        foreach (var entity in _filterInput.Value)
            //{
            //    ref var mouseClick = ref _mousePositionPool.Value.Get(entity);
            //    foreach(var entityAbility in _filter.Value)
            //    {
            //        ref var abilityAttackZone = ref _attackZonePool.Value.Get(entityAbility);
            //        if (mouseClick.pointMap.PointToMap != abilityAttackZone.Click.PointToMap) continue;
            //        _invokeAbility.Value.Add(entityAbility);
            //        _mouseClickUpPool.Value.Del(entity);
            //    }
            //}
        }
    }
}