using Leopotam.EcsLite;

namespace Client {
    struct ChangeWeaponEvent {
        public EcsPackedEntity OwnerWeapon;
        public WeaponConfig WeaponConfig;
    }
}