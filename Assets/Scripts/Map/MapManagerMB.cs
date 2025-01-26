using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManagerMB : MonoBehaviour
{
    [HideInInspector] public  Tilemap[] tilesMap;
    [HideInInspector] public GeneralTileMap generalTileMap;
    [HideInInspector] public MapAreaMovement mapAreaMovment;
    public TileBase TileArea;
    [HideInInspector] public static MapManagerMB Instance;
    private void Awake()
    {
        tilesMap = FindObjectsOfType<Tilemap>();
        tilesMap.FirstOrDefault(x => x.TryGetComponent<GeneralTileMap>(out generalTileMap));
        tilesMap.FirstOrDefault(x => x.TryGetComponent<MapAreaMovement>(out mapAreaMovment));
        Instance = this;
    }
    public List<WayToPoint> GetAreMovement(Vector3 position, int speed, LayerMask layerObstacle)
    {
        var area = generalTileMap.GetAreaMovement(position, speed, layerObstacle);
        //mapAreaMovment.ClearTile();
        //foreach (var cell in area)
        //{
        //    mapAreaMovment.SetTile(TileArea, cell);
        //}
        return area;
    }
}
