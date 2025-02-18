using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Collections.Generic;
using UnityEngine;

namespace Client {
    sealed class InitAttackZoneSystem : IEcsRunSystem {
        readonly EcsFilterInject<Inc<AbilityComponent, InitAttackZoneEvent>> _filter;
        readonly EcsPoolInject<InitAttackZoneEvent> _initAttackZonePool;
        readonly EcsPoolInject<AttackZoneComponent> _attackZoneComponent;
        readonly EcsPoolInject<AbilityComponent> _abilityComponent;
        readonly EcsPoolInject<DrawAttackZoneEvent> _drawAttackZonePool;
        readonly EcsPoolInject<ClearMapDrawerEvent> _clearMapDrawerPool;
        readonly EcsPoolInject<AttackAreaComponent> _attackAreaPool;
        readonly EcsPoolInject<TurnAttackZoneEvent> _turnAttackPool;
        readonly EcsWorldInject _world;
        public void Run (IEcsSystems systems) {
            foreach(var entity in _filter.Value)
            {
                ref var initAttackZoneComp = ref _initAttackZonePool.Value.Get(entity);
                ref var abilityComponent = ref _abilityComponent.Value.Get(entity);
                if (!_attackZoneComponent.Value.Has(entity))
                {
                    var attackZone = GetAttackPoints(abilityComponent.ability.AttackZoneConfig.matrix, initAttackZoneComp.pointCenter);
                    attackZone.Add(initAttackZoneComp.pointCenter);
                    if (attackZone is null) continue;
                    ref var attackZoneComp = ref _attackZoneComponent.Value.Add(entity);
                    attackZoneComp.Direction = 1;
                    attackZoneComp.Center = initAttackZoneComp.pointCenter;
                    attackZoneComp.pointAttack = attackZone;
                }
                else
                {
                    ref var attackZoneComponent1 = ref _attackZoneComponent.Value.Get(entity);
                    attackZoneComponent1.NewCenter(initAttackZoneComp.pointCenter.PointToMap);
                }
                ref var attackZoneComponent = ref _attackZoneComponent.Value.Get(entity);
                if (!_attackAreaPool.Value.Has(entity)) continue;
                ref var attackAreaComp = ref _attackAreaPool.Value.Get(entity);
                if (!attackAreaComp.AttackArea.ContainsKey(attackZoneComponent.Center)) continue;
                _drawAttackZonePool.Value.Add(entity);
                _clearMapDrawerPool.Value.Add(_world.Value.NewEntity());
                var permittedDirection = attackAreaComp.AttackArea[attackZoneComponent.Center];
                if (attackZoneComponent.Direction == permittedDirection || permittedDirection == 5) continue;
                _turnAttackPool.Value.Add(entity).NewDirection = permittedDirection;
            }
        }
        private  List<PointMap> GetAttackPoints(Matrix attackZone, PointMap basePoint)

        {
            if (attackZone == null || attackZone.matrix == null || attackZone.matrix.Count == 0)
                return new List<PointMap>();

            int rows = attackZone.matrix.Count;
            int cols = attackZone.matrix[0].values.Count;

            List<PointMap> result = new List<PointMap>();
            Vector3Int center = new();

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (attackZone.matrix[r].values[c] == 2) center = new Vector3Int(c, r);
                }
            }           
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    if (attackZone.matrix[r].values[c] == 1)
                    {
                        var deltaVector = center - new Vector3Int(c, r);
                        var newPointInMap = basePoint.PointToMap + new Vector3Int(-deltaVector.x, deltaVector.y, 0);
                        var PointMap = GameState.Instance.GetNewPoint(newPointInMap);
                        result.Add(PointMap);
                    }
                }
            }
            return result;
        }
    }
}