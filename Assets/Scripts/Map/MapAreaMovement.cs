using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapAreaMovement : MonoBehaviour
{
    private Tilemap _tilemap;
    private List<Vector3Int> validCell;
    private void Start()
    {
        _tilemap = GetComponent<Tilemap>();
    }
    public void SetTile(TileBase tile, Vector3Int postion)
    {
        _tilemap.SetTile(postion, tile);
    }
    public void ClearTile()
    {
        _tilemap.ClearAllTiles();
    }
}
