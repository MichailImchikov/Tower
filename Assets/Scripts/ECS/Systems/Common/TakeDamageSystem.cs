using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class TakeDamageSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<TakeDamageEvent, HealthComponent>> _filter;
        readonly EcsPoolInject<HealthComponent> _healthPool;
        readonly EcsPoolInject<TakeDamageEvent> _takeDamagePool;
        public void Run (IEcsSystems systems) {
            foreach(var entity in _filter.Value)
            {
                ref var healthComp = ref _healthPool.Value.Get(entity);
                ref var takeDamageComp = ref _takeDamagePool.Value.Get(entity);
                healthComp.Health -= takeDamageComp.Damage;
            }
        }
    }
}