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
        readonly EcsWorldInject _world;
        public void Run (IEcsSystems systems) {
            foreach(var entity in _filter.Value)
            {
                ref var initPool = ref _initAttackZonePool.Value.Get(entity);
                ref var abilityComponent = ref _abilityComponent.Value.Get(entity);
                var attackZone = GetAttackPoints(abilityComponent.ability.AttackZoneConfig.attackZone, initPool.pointCenter);
                attackZone.Add(initPool.pointCenter);
                if (attackZone is null) continue;
                if (!_attackZoneComponent.Value.Has(entity)) 
                    _attackZoneComponent.Value.Add(entity);
                ref var attackZoneComponent = ref _attackZoneComponent.Value.Get(entity);
                attackZoneComponent.pointAttack = attackZone;
                attackZoneComponent.Click = initPool.pointCenter;
                _clearMapDrawerPool.Value.Add(_world.Value.NewEntity());
                _drawAttackZonePool.Value.Add(entity);
                // todo DrawAttackZoneEvent
            }
        }
        private  List<PointMap> GetAttackPoints(AttackZone attackZone, PointMap basePoint)

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
                        var newPointInMap = basePoint.PointToMap - deltaVector;
                        var PointMap = GameState.Instance.GetNewPoint(newPointInMap);
                        result.Add(PointMap);
                    }
                }
            }
            return result;
        }
    }
}