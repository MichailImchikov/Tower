using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class CheckInitAttackZoneSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<PlayerComponent,AttackStateComponent, AbilityToUseComponent>> _filterPlayer;
        readonly EcsPoolInject<AbilityToUseComponent> _abilityToUsePool;
        readonly EcsFilterInject<Inc<MouseClickUpEvent, MousePositionComponent>> _filterInput;
        readonly EcsPoolInject<InitAttackZoneEvent> _initAttackZonePool;
        readonly EcsPoolInject<MousePositionComponent> _mousePositionPool;
        readonly EcsPoolInject<MouseClickUpEvent> _mouseClickUpPool;
        readonly EcsWorldInject _world;
        public void Run (IEcsSystems systems) {
            foreach (var entityPlayer in _filterPlayer.Value)
            {
                ref var abilityToUse = ref _abilityToUsePool.Value.Get(entityPlayer);
                foreach (var entityClick in _filterInput.Value)
                {
                    ref var mousePositionComp = ref _mousePositionPool.Value.Get(entityClick);
                    if (!abilityToUse.entityAbility.Unpack(_world.Value, out int abilityEntity)) continue;
                    ref var initAttackZoneEvent = ref _initAttackZonePool.Value.Add(abilityEntity);
                    initAttackZoneEvent.pointCenter = mousePositionComp.pointMap;
                    _mouseClickUpPool.Value.Del(entityClick);
                }
            }
        }
    }
}