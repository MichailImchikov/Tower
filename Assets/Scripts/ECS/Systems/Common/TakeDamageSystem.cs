using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class TakeDamageSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<TakeDamageEvent, HealthComponent>,Exc<DeathComponent>> _filter;
        readonly EcsPoolInject<HealthComponent> _healthPool;
        readonly EcsPoolInject<TakeDamageEvent> _takeDamagePool;
        readonly EcsPoolInject<DeathEvent> _DeathPool;
        readonly EcsPoolInject<RequestAnimationEvent> _requestAnimationPool;
        readonly EcsWorldInject _world;
        public void Run (IEcsSystems systems) {
            foreach(var entity in _filter.Value)
            {
                ref var healthComp = ref _healthPool.Value.Get(entity);
                ref var takeDamageComp = ref _takeDamagePool.Value.Get(entity);
                healthComp.Health -= takeDamageComp.Damage;
                if(healthComp.Health <= 0)
                {
                    ref var DeathComp = ref _DeathPool.Value.Add(entity);
                    ref var requestAnimationState = ref _requestAnimationPool.Value.Add(_world.Value.NewEntity());
                    requestAnimationState.State = AnimationState.Death;
                    requestAnimationState.entityPacked = _world.Value.PackEntity(entity);
                }
            }
        }
    }
}