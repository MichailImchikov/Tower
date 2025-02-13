using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class ChangeAttackStateSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<ChangeAttackStateEvent>, Exc<DeathStateComponent,MoveToPointComponent>> _filter;
        readonly EcsPoolInject<ClearMapDrawerEvent> _clearMapDrawer;
        readonly EcsPoolInject<MoveStateComponent> _moveStatePool;
        readonly EcsPoolInject<AttackStateComponent> _attackStateComponent;
        readonly EcsPoolInject<AreaWalkingComponent> _areaWalkingPool;
        readonly EcsWorldInject _world;
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                if (_moveStatePool.Value.Has(entity)) _moveStatePool.Value.Del(entity);
                if (_areaWalkingPool.Value.Has(entity)) _areaWalkingPool.Value.Del(entity);
                if (!_attackStateComponent.Value.Has(entity)) _attackStateComponent.Value.Add(entity);
                _clearMapDrawer.Value.Add(_world.Value.NewEntity());
            }

        }
    }
}