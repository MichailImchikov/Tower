using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace Client {
    sealed class CreateAreaWalkingSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<CreateAreaWalkingEvent, MovePointsComponent, TransformComponent>> _filter;
        readonly EcsPoolInject<MovePointsComponent> _movePool;
        readonly EcsPoolInject<TransformComponent> _transformPool;
        readonly EcsPoolInject<AreaWalkingComponent> _areaWolkingPool;
        private static int _layerMask = 1<<3;
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var moveComp = ref _movePool.Value.Get(entity);
                ref var transformComp = ref _transformPool.Value.Get(entity);

                var area = new Dictionary<PointMap, int> { { GameState.Instance.GetNewPoint(transformComp.Transform.position), 0 } };
                Dictionary<PointMap, List<PointMap>> obstacle = new ();
                for (int index = 0; index < moveComp.CurrentValue; index++)
                {
                    var filteredDict = area.Where(pair => pair.Value == index)
                                         .ToDictionary(pair => pair.Key, pair => pair.Value);
                    foreach (var item in filteredDict)
                    {
                        if (CheckValidWay(obstacle,item.Key.PointToWorld, item.Key.PointToWorld + Vector3.right))
                            area.TryAdd(GameState.Instance.GetNewPoint(item.Key.PointToWorld + Vector3.right), index + 1);
                        if (CheckValidWay(obstacle, item.Key.PointToWorld, item.Key.PointToWorld + Vector3.down))
                            area.TryAdd(GameState.Instance.GetNewPoint(item.Key.PointToWorld + Vector3.down), index + 1);
                        if (CheckValidWay(obstacle, item.Key.PointToWorld, item.Key.PointToWorld + Vector3.up))
                            area.TryAdd(GameState.Instance.GetNewPoint(item.Key.PointToWorld + Vector3.up), index + 1);
                        if (CheckValidWay(obstacle, item.Key.PointToWorld, item.Key.PointToWorld + Vector3.left))
                            area.TryAdd(GameState.Instance.GetNewPoint(item.Key.PointToWorld + Vector3.left), index + 1);
                    }
                }
                if (!_areaWolkingPool.Value.Has(entity)) _areaWolkingPool.Value.Add(entity);
                ref var areaWalkingComp = ref _areaWolkingPool.Value.Get(entity);
                areaWalkingComp.areaWalking = area;
                areaWalkingComp.obstacles = obstacle;
            }
        }
        private bool CheckValidWay(Dictionary<PointMap, List<PointMap>> obstacle, Vector3 startPosition, Vector3 endPosition)
        {
            var direction = (endPosition - startPosition).normalized;
            var distance = Vector2.Distance(startPosition, endPosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(startPosition, direction, distance, _layerMask);
            if (hits.Length > 0)
            {
                if (obstacle.ContainsKey(GameState.Instance.GetNewPoint(startPosition)))
                    obstacle[GameState.Instance.GetNewPoint(startPosition)].Add(GameState.Instance.GetNewPoint(endPosition));
                else
                {
                    obstacle.Add(GameState.Instance.GetNewPoint(startPosition), new List<PointMap>());
                    obstacle[GameState.Instance.GetNewPoint(startPosition)].Add(GameState.Instance.GetNewPoint(endPosition));
                }
            }
            return hits.Length == 0;
        }
    }
}