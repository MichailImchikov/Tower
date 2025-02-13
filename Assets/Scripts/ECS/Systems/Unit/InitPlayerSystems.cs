using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client {
    sealed class InitPlayerSystems : IEcsInitSystem {
        readonly EcsWorldInject _world;
        readonly EcsPoolInject<AllyComponent> _allyPool;
        readonly EcsPoolInject<TransformComponent> _transformPool;
        readonly EcsPoolInject<ChangePlayerEvent> _changePlayerPool;
        readonly EcsPoolInject<MovePointsComponent> _movePool;
        readonly EcsPoolInject<PointInMapComponent> _pointInMapPool;
        readonly EcsPoolInject<AnimatorComponent> _animatorPool;
        readonly EcsPoolInject<HealthComponent> _healthPool;
        readonly EcsPoolInject<InitAbilityEvent> _initAbility;
        readonly EcsPoolInject<AbilityContainer> _abilityContainer;
        public void Init (IEcsSystems systems) {
            var unitsAtScenes = GameObject.FindObjectsOfType<UnitMB>();
            foreach(var ally in unitsAtScenes)
            {
                var newEntity = _world.Value.NewEntity();
                ally.Entity = newEntity;
                _allyPool.Value.Add(newEntity);
                ref var transformComp = ref _transformPool.Value.Add(newEntity);
                transformComp.Transform = ally.transform;
                ref var moveComponent = ref _movePool.Value.Add(newEntity);
                moveComponent.BaseValue = ally.MaxCellMove;
                moveComponent.CurrentValue = moveComponent.BaseValue;
                ref var pointInMapComp = ref _pointInMapPool.Value.Add(newEntity);
                pointInMapComp.pointMap = GameState.Instance.GetNewPoint(transformComp.Transform.position);
                transformComp.Transform.position = pointInMapComp.pointMap.PointToWorld;
                ref var animatorComponent = ref _animatorPool.Value.Add(newEntity);
                animatorComponent.Animator = ally.GetComponentInChildren<Animator>();
                ref var healthPool = ref _healthPool.Value.Add(newEntity);
                healthPool.Health = ally.Health;
                if(ally.WeaponConfig is not null)
                {
                    ref var abilityContainer = ref _abilityContainer.Value.Add(newEntity);
                    abilityContainer.Abilities = new();
                    foreach(var abilityConfig in ally.WeaponConfig.AbilitiesConfig)
                    {
                        int abilityEntity = _world.Value.NewEntity();
                        ref var initAbil = ref _initAbility.Value.Add(abilityEntity);
                        initAbil.ability = abilityConfig.ability;
                        initAbil.packedEntityOwner = _world.Value.PackEntity(newEntity);
                        abilityContainer.Abilities.Add(_world.Value.PackEntity(abilityEntity));
                    }
                }
            }
            ref var chargePlayerComp = ref _changePlayerPool.Value.Add(_world.Value.NewEntity());
            var randomPlayer = unitsAtScenes[Random.Range(0, unitsAtScenes.Length - 1)];
            chargePlayerComp.newPlayer = _world.Value.PackEntity(randomPlayer.Entity);
            Camera.main.transform.position = randomPlayer.transform.position + Vector3.back;

        }
    }
}