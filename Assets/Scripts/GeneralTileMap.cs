using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GeneralTileMap : MonoBehaviour
{
    private Tilemap _tilemap;
    void Start()
    {
        _tilemap = GetComponent<Tilemap>();
    }
    public Vector3 GetCenterTileByPosition(Vector3 position)
    {
        Vector3Int tilePosition = _tilemap.WorldToCell(position);
        return _tilemap.GetCellCenterWorld(tilePosition);
    }
}
