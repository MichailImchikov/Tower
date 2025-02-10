using Leopotam.EcsLite;

namespace Client {
    struct InitAbilityEvent {
        public EcsPackedEntity packedEntityOwner;
        public Ability ability;
    }
}