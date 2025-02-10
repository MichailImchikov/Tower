using Leopotam.EcsLite;

namespace Client {
    struct AbilityComponent 
    {
        public EcsPackedEntity packedEntityOwner;
        public Ability ability;
    }
}