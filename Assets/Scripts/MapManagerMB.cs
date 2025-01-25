using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManagerMB : MonoBehaviour
{
    private Tilemap[] _tilesMap;
    private GeneralTileMap _generalTileMap;
    private void Start()
    {
        _tilesMap = FindObjectsOfType<Tilemap>();
        _tilesMap.FirstOrDefault(x => x.TryGetComponent<GeneralTileMap>(out _generalTileMap));
    }
    public Vector3 GetCenterGeneralTile(Vector3 position)
    {
        return _generalTileMap.GetCenterTileByPosition(position);
    }
    public List<Vector3Int> GetAreMovment(Vector3 position, int speed)
    {
        return _generalTileMap.GetAreaMovement(position, speed);
    }
}
