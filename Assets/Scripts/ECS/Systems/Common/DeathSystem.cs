using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class DeathSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<DeathEvent>> _filter;
        readonly EcsPoolInject<DeathComponent> _deathPool;
        public void Run (IEcsSystems systems) {
            foreach(var entity in _filter.Value)
            {
                ref var DeathComp = ref _deathPool.Value.Add(entity);
            }
        }
    }
}