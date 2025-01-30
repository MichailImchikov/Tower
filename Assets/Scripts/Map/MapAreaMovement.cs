using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapAreaMovement : MonoBehaviour, IObserverAction
{
    private Tilemap _tilemap;
    public TileBase TileWay;
    void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
    }
    public void SetTile(Vector3Int position)
    {
        _tilemap.SetTile(position,TileWay);
    }
    private void ClearTile()
    {
        _tilemap.ClearAllTiles();
    }

    public void UpdateStatus(StatusUnit status)
    {

    }

    public void UpdateStatus(StatusUnit status, UnitData unitData)
    {
        if (status == StatusUnit.Move)
            ClearTile();
        if (status == StatusUnit.Idle)
        {
            var area = unitData.Range_Area.CreateRangeArea(unitData.transformUnit.position);
            foreach(var item in area)
            {
                SetTile(item.Key.PointToMap);
            }
        }    
    }
}
