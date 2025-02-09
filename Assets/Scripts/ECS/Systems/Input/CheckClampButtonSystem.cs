using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class CheckClampButtonSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<MouseClickDownEvent>> _filterDown;
        readonly EcsFilterInject<Inc<MouseClickUpEvent>> _filterUp;
        readonly EcsPoolInject<TimerMouseClampComponent> _timerMouseClampPool;
        readonly EcsPoolInject<MouseClampComponent> _mouseClapmPool;
        readonly EcsPoolInject<MouseClickUpEvent> _mouseClickUpPool;
        public void Run (IEcsSystems systems)
        {
            foreach(var entity in _filterDown.Value)
            {
                ref var timerMouseClampComp = ref _timerMouseClampPool.Value.Add(entity);
                timerMouseClampComp.Timer = 0.15f;
            }
            foreach(var entity in _filterUp.Value)
            {
                if(_timerMouseClampPool.Value.Has(entity))
                    _timerMouseClampPool.Value.Del(entity);
                if (!_mouseClapmPool.Value.Has(entity)) continue;
                _mouseClickUpPool.Value.Del(entity);
                _mouseClapmPool.Value.Del(entity);
            }
        }
    }
}