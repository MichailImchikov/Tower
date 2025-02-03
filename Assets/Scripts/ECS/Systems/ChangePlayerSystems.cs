using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class ChangePlayerSystems : IEcsRunSystem {
        readonly EcsFilterInject<Inc<ChangePlayerEvent>> _filter;
        readonly EcsPoolInject<ChangePlayerEvent> _changePlayerPool;
        readonly EcsPoolInject<PlayerComponent> _playerPool;
        readonly EcsWorldInject _world;
        readonly EcsFilterInject<Inc<PlayerComponent>> _filterPlayer;
        readonly EcsPoolInject<TransformComponent> _transformPool;
        public void Run (IEcsSystems systems) {
            foreach (var entity in _filter.Value)
            {
                ref var changePlayerComp = ref _changePlayerPool.Value.Get(entity);
                int entityNewPlayer;
                if (!changePlayerComp.newPlayer.Unpack(_world.Value, out entityNewPlayer)) continue;
                foreach (var currentEntity in _filterPlayer.Value)
                {
                    _playerPool.Value.Del(currentEntity);
                }
                _playerPool.Value.Add(entityNewPlayer);
                ref var transformComp = ref _transformPool.Value.Get(entityNewPlayer);
                Camera.main.transform.SetParent(transformComp.Transform,false);
            }
        }
    }
}