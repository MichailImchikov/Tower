using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class ChangeAnimationSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<RequestAnimationEvent>> _filter;
        readonly EcsPoolInject<RequestAnimationEvent> _requestAnimationPool;
        readonly EcsPoolInject<AnimatorComponent> _animatorPool;
        readonly EcsWorldInject _world;
        public void Run (IEcsSystems systems) {
            foreach(var entity in _filter.Value)
            {
                ref var requestAnimationComp = ref _requestAnimationPool.Value.Get(entity);
                if (!requestAnimationComp.entityPacked.Unpack(_world.Value, out int entityUnit)) continue;
                if (!_animatorPool.Value.Has(entityUnit)) continue;
                ref var animatorComp = ref _animatorPool.Value.Get(entityUnit);
                animatorComp.Animator.SetTrigger(requestAnimationComp.State.ToString());
            }
        }
    }
}