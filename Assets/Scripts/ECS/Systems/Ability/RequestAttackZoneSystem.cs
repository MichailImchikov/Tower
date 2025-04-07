using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class RequestAttackZoneSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<RequestAttackZoneEvent>> _filter;
        readonly EcsPoolInject<AttackAreaComponent> _attackAreaPool;
        readonly EcsPoolInject<RequestAttackZoneEvent> _requestAttackZonePool;
        readonly EcsPoolInject<InitAttackZoneEvent> _initAttackZonePool;
        readonly EcsPoolInject<CheckDirectionAttackEvent> _checkDirectionPool;
        public void Run (IEcsSystems systems) {
            foreach(var entity in _filter.Value)
            {
                ref var requestAttackComp = ref _requestAttackZonePool.Value.Get(entity);
                if(_attackAreaPool.Value.Has(entity))
                {
                    ref var attackAreaComp = ref _attackAreaPool.Value.Get(entity);
                    if (!attackAreaComp.AttackArea.ContainsKey(requestAttackComp.PointClick)) continue;
                    ref var initAttackZoneComp = ref _initAttackZonePool.Value.Add(entity);
                    initAttackZoneComp.pointCenter = requestAttackComp.PointClick;
                    _checkDirectionPool.Value.Add(entity);
                }
                else
                {
                    ref var initAttackZoneComp1 = ref _initAttackZonePool.Value.Add(entity);// For abilities with unlimited area
                    initAttackZoneComp1.pointCenter = requestAttackComp.PointClick;
                }
            }
        }
    }
}