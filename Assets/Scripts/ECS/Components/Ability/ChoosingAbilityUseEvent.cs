using Leopotam.EcsLite;

namespace Client {
    struct ChoosingAbilityUseEvent {
        public EcsPackedEntity abilityEntity;
        public EcsPackedEntity ownerAbility;
    }
}