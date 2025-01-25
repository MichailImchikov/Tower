using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WayToPoint 
{
    public Vector3Int lastPointToMap => pointsWay.Last().PointToMap;
    public Vector3 lastPointToWorld => pointsWay.Last().PointToWorld;
    public int SizeWay => pointsWay.Count;
    private List<PointMap> pointsWay { get; set; }
    private int currentPoint;
    public PointMap GetNextPoint()
    {
        if (pointsWay.Count - 1 >= currentPoint) return null;
        currentPoint++;
        return pointsWay[currentPoint];
    }
    public WayToPoint(WayToPoint wayToPoint,PointMap pointMap)
    {
        pointsWay = new();
        wayToPoint.pointsWay.ForEach(x => pointsWay.Add(new PointMap(x)));
        var point = new PointMap (pointMap);
        pointsWay.Add(point);
        
    }
    public WayToPoint(PointMap pointMap)
    {
        pointsWay = new() { new(pointMap) } ; 
    }
    public bool CheckBestWay(WayToPoint wayToPoint)
    {
        if (wayToPoint.SizeWay > SizeWay) return false;
        pointsWay = wayToPoint.pointsWay;
        return true;
    }
}
