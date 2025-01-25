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
    private MapAreaMovement _mapAreaMovment;
    public TileBase TileArea;
    private void Start()
    {
        _tilesMap = FindObjectsOfType<Tilemap>();
        _tilesMap.FirstOrDefault(x => x.TryGetComponent<GeneralTileMap>(out _generalTileMap));
        _tilesMap.FirstOrDefault(x => x.TryGetComponent<MapAreaMovement>(out _mapAreaMovment));
    }
    public List<Vector3Int> GetAreMovement(Vector3 position, int speed, LayerMask layerObstacle)
    {
        var area = _generalTileMap.GetAreaMovement(position, speed, layerObstacle);
        _mapAreaMovment.ClearTile();
        foreach (var cell in area)
        {
            _mapAreaMovment.SetTile(TileArea, cell);
        }
        return area;
    }
    public Vector3? CheckValidPoint(Vector3 newPoint, List<Vector3Int> validPoints )
    {
        return _generalTileMap.CheckValidPoint(newPoint, validPoints);
    }
}
