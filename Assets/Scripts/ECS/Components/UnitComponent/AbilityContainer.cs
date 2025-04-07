using Leopotam.EcsLite;
using System.Collections.Generic;

namespace Client {
    struct AbilityContainer {
        public List<EcsPackedEntity> Abilities;
        public void RemoveAbilities(EcsWorld world)
        {
            var attackAteaPool = world.GetPool<AttackAreaComponent>();
            var attackZonePool = world.GetPool<AttackZoneComponent>();  
            foreach(var abilityPacked in Abilities)
            {
                if (!abilityPacked.Unpack(world, out int abilityEntity)) continue;
                if (attackZonePool.Has(abilityEntity))
                {
                    attackZonePool.Del(abilityEntity);
                }
                if(attackAteaPool.Has(abilityEntity))
                {
                    attackAteaPool.Del(abilityEntity);
                }
            }
        }
    }
}