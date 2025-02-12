using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class CheckInitAttackZoneSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<MouseClickUpEvent, MousePositionComponent>> _filterInput;
        readonly EcsFilterInject<Inc<AbilityComponent, AbilityToUseComponent>> _filterAbility;
        readonly EcsPoolInject<InitAttackZoneEvent> _initAttackZonePool;
        readonly EcsPoolInject<MousePositionComponent> _mousePositionPool;
        readonly EcsPoolInject<MouseClickUpEvent> _mouseClickUpPool;
        public void Run (IEcsSystems systems) {
            foreach(var entityClick in _filterInput.Value)
            {
                ref var mousePositionComp = ref _mousePositionPool.Value.Get(entityClick);
                foreach(var entityAbility in _filterAbility.Value)
                {
                    ref var initAttackZoneEvent = ref _initAttackZonePool.Value.Add(entityAbility);
                    initAttackZoneEvent.pointCenter = mousePositionComp.pointMap;
                    _mouseClickUpPool.Value.Del(entityClick);
                }
            }
        }
    }
}