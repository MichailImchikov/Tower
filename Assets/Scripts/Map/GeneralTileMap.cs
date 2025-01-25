
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
    public List<Vector3Int> GetAreaMovement(Vector3 position,int speed,LayerMask layerObstacle)
    {
        List<Vector3Int> areaMovement = new();
        var positionInt = _tilemap.WorldToCell(position);
        areaMovement.Add(positionInt);
        CreateAreaMovement(positionInt, speed, areaMovement, layerObstacle);
        return areaMovement;
    }
    private void CreateAreaMovement(Vector3Int posotion, int speed, List<Vector3Int> area, LayerMask _layerMask)
    {
        if(speed > 0)
        {
            CheckArea(posotion, speed, area, Vector3Int.down, _layerMask);
            CheckArea(posotion, speed, area, Vector3Int.up, _layerMask);
            CheckArea(posotion, speed, area, Vector3Int.left, _layerMask);
            CheckArea(posotion, speed, area, Vector3Int.right, _layerMask);
        }
    }
    private void CheckArea(Vector3Int posotion, int speed, List<Vector3Int> area, Vector3Int offset,LayerMask _layerMask)
    {
        var newPosition = posotion + offset;
        if (!_tilemap.HasTile(newPosition)) return;
            if (!CheckValidWay(_tilemap.GetCellCenterWorld(posotion), _tilemap.GetCellCenterWorld(newPosition), _layerMask)) return;
            var newTile = _tilemap.GetTile(newPosition);
            area.Add(newPosition);
            CreateAreaMovement(newPosition, speed - 1, area, _layerMask);
    }
    public bool CheckValidWay(Vector3 startPosition, Vector3 endPosition, LayerMask layerMaskObstacle)
    {
        var direction = (endPosition - startPosition).normalized;
        var distance = Vector2.Distance(startPosition, endPosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(startPosition, direction, distance, layerMaskObstacle);
        return hits.Length == 0;
    }
    public Vector3? CheckValidPoint(Vector3 newPosition, List<Vector3Int> validPoints)
    {
        var pointCell = _tilemap.WorldToCell(newPosition);
        if (validPoints.Contains(pointCell))
            return _tilemap.GetCellCenterWorld(pointCell);
        return null;
    }
}
