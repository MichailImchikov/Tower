using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinder
{
    private RangeArea _area;
    private List<PointMap> _points;
    private Dictionary<PointMap, int> rangeArea;
    public List<PointMap> pointMaps=> _points;
    public Pathfinder(RangeArea area)
    {
        _area = area;
    }
    private void CheakObstacleOnMyWay(UnitData unitData, int index, Vector3 direction, ref Vector3 endPos)
    {
        if (rangeArea.ContainsKey(new PointMap(endPos + direction, unitData.Range_Area.Tilemap)))
        {
            if (rangeArea[new PointMap(endPos + direction, unitData.Range_Area.Tilemap)] == index - 1)
            {
                if (unitData.Range_Area.Obstacle.ContainsKey(new PointMap(endPos + direction, unitData.Range_Area.Tilemap)))
                {
                    if (!unitData.Range_Area.Obstacle[new PointMap(endPos + direction, unitData.Range_Area.Tilemap)].Contains(new PointMap(endPos, unitData.Range_Area.Tilemap))) endPos = endPos + direction;
                }
                else endPos = endPos + direction;
            }
        }
    }
    public List<PointMap> CreatePath(Vector3 endPosition, UnitData unitData)
    {
        Vector3 endPos = endPosition;
        int index;
        Tilemap tilemap= unitData.Range_Area.Tilemap;
        _points = new List<PointMap>
        {
            new PointMap(endPos, tilemap)
        };
        rangeArea = unitData.Range_Area.Area;
        do
        {
            var position = new PointMap(endPos, tilemap);
            index = rangeArea[position];
            CheakObstacleOnMyWay(unitData, index, Vector3.right, ref endPos);
            CheakObstacleOnMyWay(unitData, index, Vector3.left, ref endPos);
            CheakObstacleOnMyWay(unitData, index, Vector3.up, ref endPos);
            CheakObstacleOnMyWay(unitData, index, Vector3.down, ref endPos);
            _points.Add(new PointMap(endPos, tilemap));
        } while (index != 1);
        _points.Reverse();
        return _points;
    }
}
