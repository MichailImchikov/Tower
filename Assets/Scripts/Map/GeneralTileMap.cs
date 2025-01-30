
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GeneralTileMap : MonoBehaviour, IObserverAction
{
    private Tilemap _tilemap;
    public static GeneralTileMap Instance;
    void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
        Instance = this;
        
    }
    public PointMap newPoint(Vector3 pointToWorld) => new(pointToWorld, _tilemap);
    public PointMap newPoint(Vector3Int pointToMap) => new(pointToMap, _tilemap);
    public void UpdateStatus(StatusUnit status, UnitData unitData)
    {
        if (status == StatusUnit.Idle)
        {
            unitData.Range_Area = new RangeArea(_tilemap, unitData.MaxCellMove, unitData._layerMaskObstacle);
        }
    }
}
