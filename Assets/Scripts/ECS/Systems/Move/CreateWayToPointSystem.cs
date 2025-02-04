using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Client {
    sealed class CreateWayToPointSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<CreateWayToPointEvent, AreaWalkingComponent>> _filter;
        readonly EcsPoolInject<CreateWayToPointEvent> _createWayToPointPool;
        readonly EcsPoolInject<AreaWalkingComponent> _areWalkingPool;
        readonly EcsPoolInject<MoveToPointComponent> _moveToPointPool;
        private Dictionary<PointMap, int> rangeArea;
        public Dictionary<PointMap, List<PointMap>> obstacles;
        public void Run(IEcsSystems systems)
        {
            foreach(var entity in _filter.Value)
            {
                ref var pointMapComp = ref _createWayToPointPool.Value.Get(entity);
                ref var areaWalkigComp = ref _areWalkingPool.Value.Get(entity);
                Vector3 endPos = pointMapComp.pointMap.PointToWorld;
                int index;
                List<PointMap> _points = new() { GameState.Instance.GetNewPoint(endPos)};
                rangeArea = areaWalkigComp.areaWalking;
                obstacles = areaWalkigComp.obstacles;
                do
                {
                    var position = GameState.Instance.GetNewPoint(endPos);
                    index = rangeArea[position];
                    CheakObstacleOnMyWay(index, Vector3.right, ref endPos);
                    CheakObstacleOnMyWay(index, Vector3.left, ref endPos);
                    CheakObstacleOnMyWay(index, Vector3.up, ref endPos);
                    CheakObstacleOnMyWay(index, Vector3.down, ref endPos);
                    _points.Add(GameState.Instance.GetNewPoint(endPos));
                } while (index != 1);
                _points.Reverse();
                ref var moveToPointComp = ref _moveToPointPool.Value.Add(entity);
                moveToPointComp.WayToPoint = _points;
                moveToPointComp.IndexCurrentPoint = 0;
            }
        }
        private void CheakObstacleOnMyWay( int index, Vector3 direction, ref Vector3 endPos)
        {
            var newPoint = GameState.Instance.GetNewPoint(endPos + direction);
            if (!rangeArea.ContainsKey(newPoint)) return;
            if (rangeArea[newPoint] != index - 1) return;
            if (obstacles.ContainsKey(newPoint))
            {
                if (!obstacles[newPoint].Contains(GameState.Instance.GetNewPoint(endPos))) 
                    endPos = endPos + direction;
            }
            else endPos = endPos + direction;
        }
    }
}