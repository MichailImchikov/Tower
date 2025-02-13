using UnityEngine;
using UnityEngine.Tilemaps;
public struct PointMap
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
}
