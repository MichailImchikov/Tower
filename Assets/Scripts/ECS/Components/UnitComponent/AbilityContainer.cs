using Leopotam.EcsLite;
using System.Collections.Generic;

namespace Client {
    struct AbilityContainer {
        public List<EcsPackedEntity> Abilities;
        public void RemoveAbilities(EcsWorld world)
        {
            var abilityToUsePool = world.GetPool<AbilityToUseComponent>();
            var attackZonePool = world.GetPool<AttackZoneComponent>();  
            foreach(var abilityPacked in Abilities)
            {
                if (!abilityPacked.Unpack(world, out int abilityEntity)) continue;
                if (abilityToUsePool.Has(abilityEntity))
                {
                    abilityToUsePool.Del(abilityEntity);
                }
                if (attackZonePool.Has(abilityEntity))
                {
                    attackZonePool.Del(abilityEntity);
                }
            }
        }
    }
}