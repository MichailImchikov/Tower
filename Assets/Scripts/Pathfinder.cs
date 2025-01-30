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
    public List<PointMap> CreatePath(Vector3 startPosition, Vector3 endPosition, Tilemap tilemap)
    {
        Vector3 endPos = endPosition;
        int index;
        _points = new List<PointMap>
        {
            new PointMap(endPosition, tilemap)
        };
        rangeArea = _area.CreateRangeArea(startPosition);
        do
        {
            var position = new PointMap(endPos, tilemap);
            index = rangeArea[position];
            if (rangeArea.ContainsKey(new PointMap(endPos + Vector3.right, tilemap)))
            {
                if (rangeArea[new PointMap(endPos + Vector3.right, tilemap)] == index-1)
                {
                    endPos = endPos + Vector3.right;
                    //break;
                }
            }
            if(rangeArea.ContainsKey(new PointMap(endPos + Vector3.left, tilemap)))
            {
                if (rangeArea[new PointMap(endPos + Vector3.left, tilemap)] == index-1)
                {
                    endPos = endPos + Vector3.left;
                    //break;
                }
            }
            if(rangeArea.ContainsKey(new PointMap(endPos + Vector3.up, tilemap)))
            {
                if (rangeArea[new PointMap(endPos + Vector3.up, tilemap)] == index - 1)
                {
                    endPos = endPos + Vector3.up;
                    //break;
                }
            }
            if(rangeArea.ContainsKey(new PointMap(endPos + Vector3.down, tilemap)))
            {
                if (rangeArea[new PointMap(endPos + Vector3.down, tilemap)] == index - 1)
                {
                    endPos = endPos + Vector3.down;
                   //break;
                }
            }
            _points.Add(new PointMap(endPos, tilemap));
        } while (index != 1);
        _points.Reverse();
        return _points;
    }
}
