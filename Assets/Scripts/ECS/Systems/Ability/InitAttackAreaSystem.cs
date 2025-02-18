using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Collections.Generic;
using UnityEngine;

namespace Client {
    sealed class InitAttackAreaSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<ChoosingAbilityUseEvent>> _filter;
        readonly EcsPoolInject<ChoosingAbilityUseEvent> _choosingAbilityUsePool;
        readonly EcsPoolInject<AbilityComponent> _abilityPool;
        readonly EcsPoolInject<PointInMapComponent> _pointInMap;
        readonly EcsPoolInject<AttackAreaComponent> _attackAreaComponent;
        readonly EcsWorldInject _world;
        public void Run (IEcsSystems systems) {
            foreach(var enity in _filter.Value )
            {
                ref var choosingAbilityComp = ref _choosingAbilityUsePool.Value.Get(enity);
                if (!choosingAbilityComp.abilityEntity.Unpack(_world.Value, out int abilityEnity)) continue;
                if (!choosingAbilityComp.ownerAbility.Unpack(_world.Value, out int ownerEntity)) continue;
                ref var abilityComp = ref _abilityPool.Value.Get(abilityEnity);
                ref var pointInMapComp = ref _pointInMap.Value.Get(ownerEntity);
                var AttackArea = GetAttackArea(abilityComp.ability.AttackAreaConfig.matrix, pointInMapComp.pointMap);
                if (!_attackAreaComponent.Value.Has(abilityEnity)) _attackAreaComponent.Value.Add(abilityEnity);
                ref var attackAreaComponent = ref _attackAreaComponent.Value.Get(abilityEnity);
                attackAreaComponent.AttackArea = AttackArea;
            }
        }
        private Dictionary<PointMap, int> GetAttackArea(Matrix attackZone, PointMap basePoint)

        {
            if (attackZone == null || attackZone.matrix == null || attackZone.matrix.Count == 0)
                return null;

            int rows = attackZone.matrix.Count;
            int cols = attackZone.matrix[0].values.Count;

            var result = new Dictionary<PointMap, int>();
            Vector3Int center = new();

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (attackZone.matrix[r].values[c] == -1) center = new Vector3Int(c, r);
                }
            }
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (attackZone.matrix[r].values[c] != 0)
                    {
                        var deltaVector = center - new Vector3Int(c, r);
                        var newPointInMap = basePoint.PointToMap + new Vector3Int(-deltaVector.x,deltaVector.y,0);
                        var PointMap = GameState.Instance.GetNewPoint(newPointInMap);
                        result.Add(PointMap, attackZone.matrix[r].values[c]);
                    }
                }
            }
            return result;
        }
    }
}