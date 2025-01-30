using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Timeline.TimelinePlaybackControls;
public class PointMap
{
    private Vector3 _pointToWorld;
    private Vector3Int _pointToMap;
    public Vector3 PointToWorld => _pointToWorld;
    public Vector3Int PointToMap => _pointToMap;
    public PointMap(Vector3 pointToWorld, Tilemap tileMap)
    {
        _pointToMap = tileMap.WorldToCell(pointToWorld);
        _pointToWorld = tileMap.GetCellCenterWorld(_pointToMap);
    }
    public PointMap(Vector3Int pointToMap, Tilemap tileMap)
    {
        _pointToMap = pointToMap;
        _pointToWorld = tileMap.GetCellCenterWorld(pointToMap);
    }
    public PointMap(PointMap clone)
    {
        _pointToWorld = clone._pointToWorld;
        _pointToMap = clone._pointToMap;
    }
    public override bool Equals(object obj)
    { return (obj != null && obj is PointMap) ? this.Equals(obj as PointMap) : false; }
    protected bool Equals(PointMap other)
    { return Equals(_pointToMap, other.PointToMap) && Equals(_pointToWorld, other.PointToWorld); }
    public override int GetHashCode()
    { unchecked { return ((_pointToMap != null ? _pointToMap.GetHashCode() : 0) * 397) ^ (_pointToWorld != null ? _pointToWorld.GetHashCode() : 0); } }
}
