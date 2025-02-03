using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class InitPlayerSystems : IEcsInitSystem {
        readonly EcsWorldInject _world;
        readonly EcsPoolInject<AllyComponent> _allyPool;
        readonly EcsPoolInject<TransformComponent> _transformPool;
        readonly EcsPoolInject<ChangePlayerEvent> _changePlayerPool;
        readonly EcsPoolInject<MoveComponent> _movePool;
        public void Init (IEcsSystems systems) {
            var unitsAtScenes = GameObject.FindObjectsOfType<UnitMB>();
            foreach(var ally in unitsAtScenes)
            {
                var newEntity = _world.Value.NewEntity();
                ally.Entity = newEntity;
                _allyPool.Value.Add(newEntity);
                ref var transformComp = ref _transformPool.Value.Add(newEntity);
                transformComp.transform = ally.transform;
                ref var moveComponent = ref _movePool.Value.Add(newEntity);
                moveComponent.MaxCellMove = ally.MaxCellMove;
            }
            ref var chargePlayerComp = ref _changePlayerPool.Value.Add(_world.Value.NewEntity());
            var randomPlayer = unitsAtScenes[Random.Range(0, unitsAtScenes.Length - 1)];
            chargePlayerComp.newPlayer = _world.Value.PackEntity(randomPlayer.Entity);
        }
    }
}