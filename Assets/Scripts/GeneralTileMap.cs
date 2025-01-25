using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GeneralTileMap : MonoBehaviour
{
    private Tilemap _tilemap;
    public Color fadedColor = new Color(1f, 1f, 1f, 0.5f);
    void Start()
    {
        _tilemap = GetComponent<Tilemap>();
    }
    public Vector3 GetCenterTileByPosition(Vector3 position)
    {
        Vector3Int tilePosition = _tilemap.WorldToCell(position);
        return _tilemap.GetCellCenterWorld(tilePosition);
    }
    public List<Vector3Int> GetAreaMovement(Vector3 position,int speed)
    {
        List<Vector3Int> areaMovement = new();
        var positionInt = _tilemap.WorldToCell(position);
        CreateAreaMovement(positionInt, speed, areaMovement);
        foreach(var tilePosition in areaMovement)
        {
            _tilemap.SetColor(tilePosition, fadedColor);
            _tilemap.RefreshTile(tilePosition);
        }
        return areaMovement;
    }
    private void CreateAreaMovement(Vector3Int posotion, int speed, List<Vector3Int> area)
    {
        if(speed > 0)
        {
            CheckArea(posotion, speed, area, Vector3Int.right);
            CheckArea(posotion, speed, area, Vector3Int.down);
            CheckArea(posotion, speed, area, Vector3Int.up);
            CheckArea(posotion, speed, area, Vector3Int.left);
        }
    }
    private void CheckArea(Vector3Int posotion, int speed, List<Vector3Int> area, Vector3Int offset)
    {
        var newPosition = posotion + offset;
        if (_tilemap.HasTile(newPosition))
        {
            var newTile = _tilemap.GetTile(newPosition);
            if (!area.Contains(newPosition))
            {
                area.Add(newPosition);
                CreateAreaMovement(newPosition, speed - 1, area);
            }
        }
    }
}
