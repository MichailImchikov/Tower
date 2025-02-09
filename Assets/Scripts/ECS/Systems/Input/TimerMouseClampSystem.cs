using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class TimerMouseClampSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<TimerMouseClampComponent>> _filter;
        readonly EcsPoolInject<TimerMouseClampComponent> _timerMousePool;
        readonly EcsPoolInject<MouseClampComponent> _mouseClampComponent;
        public void Run (IEcsSystems systems) {
            foreach(var entity in _filter.Value)
            {
                ref var timerComp = ref _timerMousePool.Value.Get(entity);
                timerComp.Timer -= Time.deltaTime;
                if (timerComp.Timer >= 0) continue;
                _timerMousePool.Value.Del(entity);
                _mouseClampComponent.Value.Add(entity);
            }
        }
    }
}