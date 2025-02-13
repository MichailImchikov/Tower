using Leopotam.EcsLite;

namespace Client {
    struct AbilityComponent 
    {
        public EcsPackedEntity packedEntityOwner;
        public bool IsOn;
        public Ability ability;
    }
}