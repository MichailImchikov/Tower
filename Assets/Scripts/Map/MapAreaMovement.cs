using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapAreaMovement : AMap
{
    public void SetTile(TileBase tile, WayToPoint way)
    {
        _tilemap.SetTile(way.lastPointToMap, tile);
    }
    public void ClearTile()
    {
        _tilemap.ClearAllTiles();
    }
}
