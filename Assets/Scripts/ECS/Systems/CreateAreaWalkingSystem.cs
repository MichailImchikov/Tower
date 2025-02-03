using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UIElements;

namespace Client {
    sealed class CreateAreaWalkingSystem : IEcsRunSystem
    {
        readonly EcsFilterInject<Inc<CreateAreaWalkingEvent, MoveComponent, TransformComponent>> _filter;
        readonly EcsPoolInject<MoveComponent> _movePool;
        readonly EcsPoolInject<TransformComponent> _transformPool;
        readonly EcsFilterInject<Inc<MapAreaDrawerComponent, MapAreaWalkingComponent>> _filterMap;
        public void Run(IEcsSystems systems)
        {
            foreach(var entity in _filter.Value)
            {
                ref var moveComp = ref _movePool.Value.Get(entity);
                int entityMap = _filterMap.Value.GetRawEntities()[0];
                //var area = new Dictionary<PointMap, int> { { new PointMap(position, _tilemap), 0 } };
                //obstacle = new Dictionary<PointMap, List<PointMap>>();
                //for (int index = 0; index < _distance; index++)
                //{
                //    var filteredDict = area.Where(pair => pair.Value == index)
                //                         .ToDictionary(pair => pair.Key, pair => pair.Value);
                //    foreach (var item in filteredDict)
                //    {
                //        if (CheckValidWay(item.Key.PointToWorld, item.Key.PointToWorld + Vector3.right))
                //            area.TryAdd(new PointMap(item.Key.PointToWorld + Vector3.right, _tilemap), index + 1);
                //        if (CheckValidWay(item.Key.PointToWorld, item.Key.PointToWorld + Vector3.down))
                //            area.TryAdd(new PointMap(item.Key.PointToWorld + Vector3.down, _tilemap), index + 1);
                //        if (CheckValidWay(item.Key.PointToWorld, item.Key.PointToWorld + Vector3.up))
                //            area.TryAdd(new PointMap(item.Key.PointToWorld + Vector3.up, _tilemap), index + 1);
                //        if (CheckValidWay(item.Key.PointToWorld, item.Key.PointToWorld + Vector3.left))
                //            area.TryAdd(new PointMap(item.Key.PointToWorld + Vector3.left, _tilemap), index + 1);
                //    }
                //}
                //_area = area;
                //return area;

            }
        }
    }
}