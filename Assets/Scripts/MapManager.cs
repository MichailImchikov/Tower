using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
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
}
