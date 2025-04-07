using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class RequestTurnAttackSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<RequestTurnAttackEvent, AttackZoneComponent>> _filter;
        readonly EcsPoolInject<AttackZoneComponent> _attackZonePool;
        readonly EcsPoolInject<AttackAreaComponent> _attackAreaPool;
        readonly EcsPoolInject<DrawAttackZoneEvent> _drawAttackZonePool;
        readonly EcsPoolInject<ClearMapDrawerEvent> _clearMapDrawerPool;
        readonly EcsWorldInject _world;
        public void Run (IEcsSystems systems) {
            foreach(var entity in _filter.Value)
            {
                ref var attackZoneComp = ref _attackZonePool.Value.Get(entity);
                if(_attackAreaPool.Value.Has(entity))
                {
                    ref var attackAreaComp = ref _attackAreaPool.Value.Get(entity);
                    var direction = attackAreaComp.AttackArea[attackZoneComp.Center];
                    if (direction != 5) continue;
                    attackZoneComp.Trun();
                    if (!_drawAttackZonePool.Value.Has(entity)) _drawAttackZonePool.Value.Add(entity);
                    _clearMapDrawerPool.Value.Add(_world.Value.NewEntity());
                }
                else
                {
                    attackZoneComp.Trun();
                }
            }
        }
    }
}