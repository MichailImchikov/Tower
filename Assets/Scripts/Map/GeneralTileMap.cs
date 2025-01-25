
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GeneralTileMap : AMap
{
    public List<WayToPoint> GetAreaMovement(Vector3 position,int speed,LayerMask layerObstacle)
    {
        List<WayToPoint> areaMovement = new();
        var newWayToPoint = new WayToPoint(newPoint(position));
        areaMovement.Add(newWayToPoint);
        CreateAreaMovement(newWayToPoint,speed, areaMovement, layerObstacle);
        return areaMovement;
    }
    private void CreateAreaMovement(WayToPoint newWayToPoint,int speed, List<WayToPoint> wayPoint, LayerMask _layerMask)
    {
        if(speed > 0)
        {
            CheckArea(newWayToPoint,speed, wayPoint, Vector3Int.down, _layerMask);
            CheckArea(newWayToPoint,speed, wayPoint, Vector3Int.up, _layerMask);
            CheckArea(newWayToPoint,speed, wayPoint, Vector3Int.left, _layerMask);
            CheckArea(newWayToPoint,speed, wayPoint, Vector3Int.right, _layerMask);
        }
    }
    private void CheckArea(WayToPoint WayToPoint,int speed, List<WayToPoint> area, Vector3Int offset,LayerMask _layerMask)
    {
        var newPos = WayToPoint.lastPointToMap + offset;
        var newPos2 = new Vector3Int(newPos.x, newPos.y,0);
        var newPosition = newPoint(WayToPoint.lastPointToMap + offset);
        WayToPoint newWayToPoint = new(WayToPoint,newPosition);
        if (_tilemap.GetTile(newPos2) == null) return;
        if (!CheckValidWay(WayToPoint.lastPointToWorld, newWayToPoint.lastPointToWorld, _layerMask)) return;
        var oldWay = area.FirstOrDefault(x => x.lastPointToMap == newWayToPoint.lastPointToMap);
        if (oldWay is not null)
            oldWay.CheckBestWay(newWayToPoint);
        else
        {
            area.Add(newWayToPoint);
            CreateAreaMovement(newWayToPoint, speed - 1, area, _layerMask);
        }
    }
    public bool CheckValidWay(Vector3 startPosition, Vector3 endPosition, LayerMask layerMaskObstacle)
    {
        var direction = (endPosition - startPosition).normalized;
        var distance = Vector2.Distance(startPosition, endPosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(startPosition, direction, distance, layerMaskObstacle);
        return hits.Length == 0;
    }
}
