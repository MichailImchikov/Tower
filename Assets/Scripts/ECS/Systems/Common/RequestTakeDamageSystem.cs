using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class RequestTakeDamageSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<RequestDamageEvent>> _filterEvent;
        readonly EcsPoolInject<RequestDamageEvent> _requestDamagePool;
        readonly EcsPoolInject<TakeDamageEvent> _TakeDamagePool;
        readonly EcsPoolInject<PointInMapComponent> _pointMapPool;
        readonly EcsPoolInject<RequestAnimationEvent> _requestAnimationPool;
        readonly EcsFilterInject<Inc<HealthComponent, PointInMapComponent>> _filterUnit;
        readonly EcsWorldInject _world;
        public void Run (IEcsSystems systems) {
            foreach(var entityEvent in _filterEvent.Value)
            {
                ref var requestDamageComp = ref _requestDamagePool.Value.Get(entityEvent);
                foreach(var entityUnit in _filterUnit.Value)
                {
                    ref var pointMapComponent = ref _pointMapPool.Value.Get(entityUnit);
                    if(pointMapComponent.pointMap.PointToMap == requestDamageComp.PointInMap.PointToMap)
                    {
                        ref var takeDamageComp = ref _TakeDamagePool.Value.Add(entityUnit);
                        takeDamageComp.Damage = requestDamageComp.Damage;
                        ref var requestAnimationState = ref _requestAnimationPool.Value.Add(_world.Value.NewEntity());
                        requestAnimationState.State = AnimationState.TakeDamage;
                        requestAnimationState.entityPacked = _world.Value.PackEntity(entityUnit);
                    }
                }
            }
        }
    }
}