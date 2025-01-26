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
    public void SetTile(WayToPoint way)
    {
        _tilemap.SetTile(way.lastPointToMap,TileWay);
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
            foreach(var way in unitData.listValidPointForMove)
            {
                SetTile(way);
            }
        }    
    }
}
