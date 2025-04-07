using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client {
    sealed class ChangeWeaponSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<ChangeWeaponEvent>> _filter;
        readonly EcsPoolInject<ChangeWeaponEvent> _changeWeaponPool;
        readonly EcsPoolInject<AbilityContainer> _abilityContainer;
        readonly EcsPoolInject<DeathStateComponent> _deadStatePool;
        readonly EcsPoolInject<InitAbilityEvent> _initAbilityPool;
        readonly EcsPoolInject<WeaponViewComponent> _weaponViewPool;
        readonly EcsWorldInject _world;
        public void Run (IEcsSystems systems) {
            foreach(var entity in _filter.Value)
            {
                ref var changeWeaponComp = ref _changeWeaponPool.Value.Get(entity);
                if (!changeWeaponComp.OwnerWeapon.Unpack(_world.Value, out int entityUnit)) continue;
                if (_deadStatePool.Value.Has(entityUnit)) continue;

                if (!_abilityContainer.Value.Has(entityUnit)) _abilityContainer.Value.Add(entityUnit);
                ref var abilityContainer = ref _abilityContainer.Value.Get(entityUnit);
                abilityContainer.Abilities = new();
                foreach (var abilityConfig in changeWeaponComp.WeaponConfig.AbilitiesConfig)
                {
                    int abilityEntity = _world.Value.NewEntity();
                    ref var initAbil = ref _initAbilityPool.Value.Add(abilityEntity);
                    initAbil.ability = abilityConfig.ability;
                    initAbil.packedEntityOwner = _world.Value.PackEntity(entityUnit);
                    abilityContainer.Abilities.Add(_world.Value.PackEntity(abilityEntity));
                }
                ref var weaponViewComp = ref _weaponViewPool.Value.Get(entityUnit);
                weaponViewComp.ChangeSprite(changeWeaponComp.WeaponConfig.Arm, changeWeaponComp.WeaponConfig.Equipment, changeWeaponComp.WeaponConfig.WeaponSprite);
            }
        }
    }
}