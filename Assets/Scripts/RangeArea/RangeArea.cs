using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

public class RangeArea
{
    private Tilemap _tilemap;
    private int _distance;
    private LayerMask _layerMask;
    private Dictionary<PointMap, int> _area;
    public Dictionary<PointMap,int>Area=> _area;
    public Tilemap Tilemap => _tilemap;
    private Dictionary<PointMap, List<PointMap>> obstacle;
    public Dictionary<PointMap, List<PointMap>> Obstacle => obstacle;
    public RangeArea(Tilemap tilemap, int distance, LayerMask layerMask)
    {
        _distance = distance;
        _tilemap = tilemap;
        _layerMask= layerMask;
    }
    public Dictionary<PointMap, int>CreateRangeArea(Vector3 position)
    {
        var area = new Dictionary<PointMap, int>
        {
            { new PointMap(position,_tilemap), 0 }
        };
        obstacle = new Dictionary<PointMap, List<PointMap>>();
        for(int index = 0; index < _distance; index++)
        {
            var filteredDict = area.Where(pair => pair.Value == index)
                                 .ToDictionary(pair => pair.Key, pair => pair.Value);
            foreach (var item in filteredDict)
            {
                if(CheckValidWay(item.Key.PointToWorld, item.Key.PointToWorld + Vector3.right))
                    area.TryAdd(new PointMap(item.Key.PointToWorld + Vector3.right, _tilemap), index+1);
                if (CheckValidWay(item.Key.PointToWorld, item.Key.PointToWorld + Vector3.down))
                    area.TryAdd(new PointMap(item.Key.PointToWorld + Vector3.down, _tilemap), index+1);
                if (CheckValidWay(item.Key.PointToWorld, item.Key.PointToWorld + Vector3.up))
                    area.TryAdd(new PointMap(item.Key.PointToWorld + Vector3.up, _tilemap), index+1);
                if (CheckValidWay(item.Key.PointToWorld, item.Key.PointToWorld + Vector3.left))
                    area.TryAdd(new PointMap(item.Key.PointToWorld + Vector3.left, _tilemap), index+1);
            }
        }
        _area = area;
        return area;
    }
    
    private bool CheckValidWay(Vector3 startPosition, Vector3 endPosition)
    {
        var direction = (endPosition - startPosition).normalized;
        var distance = Vector2.Distance(startPosition, endPosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(startPosition, direction, distance, _layerMask);
        if (hits.Length > 0)
        {
            if (obstacle.ContainsKey(new PointMap(startPosition,_tilemap))) 
                obstacle[new PointMap(startPosition, _tilemap)].Add(new PointMap(endPosition, _tilemap));
            else
            {
                obstacle.Add(new PointMap(startPosition, _tilemap), new List<PointMap>());
                obstacle[new PointMap(startPosition, _tilemap)].Add(new PointMap(endPosition, _tilemap));
            }
        }
        return hits.Length == 0;
    }
}
