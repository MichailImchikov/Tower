using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class ChangeMoveStateSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<ChangeMoveStateEvent>,Exc<DeathStateComponent,MoveToPointComponent>> _filter;
        readonly EcsPoolInject<CreateAreaWalkingEvent> _areaWalkingPool;
        readonly EcsPoolInject<MoveStateComponent> _moveStatePool;
        readonly EcsPoolInject<AttackStateComponent> _attackStateComponent;
        readonly EcsPoolInject<PlayerComponent> _player;
        readonly EcsPoolInject<DrawAreaWalkingEvent> _drawerAreaWalkingPool;
        readonly EcsPoolInject<AbilityContainer> _abilityContainerPool;
        readonly EcsWorldInject _world;
        public void Run (IEcsSystems systems) {
            foreach(var entity in _filter.Value)
            {
                if (_attackStateComponent.Value.Has(entity)) _attackStateComponent.Value.Del(entity);
                if(_abilityContainerPool.Value.Has(entity))
                {
                    ref var abilityContainer = ref _abilityContainerPool.Value.Get(entity);
                    abilityContainer.RemoveAbilities(_world.Value);
                }
                if(!_moveStatePool.Value.Has(entity)) _moveStatePool.Value.Add(entity);
                if (!_areaWalkingPool.Value.Has(entity)) _areaWalkingPool.Value.Add(entity);
                if (_player.Value.Has(entity)) _drawerAreaWalkingPool.Value.Add(entity);
            }

        }
    }
}