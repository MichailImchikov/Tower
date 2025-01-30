
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GeneralTileMap : MonoBehaviour, IObserverAction
{
    private Tilemap _tilemap;
    public static GeneralTileMap Instance;
    void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
        Instance = this;
        
    }
    public PointMap newPoint(Vector3 pointToWorld) => new(pointToWorld, _tilemap);
    public PointMap newPoint(Vector3Int pointToMap) => new(pointToMap, _tilemap);
    //public List<WayToPoint> GetAreaMovement(Vector3 position, int speed, LayerMask layerObstacle)
    //{
    //    List<WayToPoint> areaMovement = new();
    //    var newWayToPoint = new WayToPoint(newPoint(position));
    //    areaMovement.Add(newWayToPoint);
    //    CreateAreaMovement(newWayToPoint, speed, areaMovement, layerObstacle);
    //    return areaMovement;
    //}
    //private void CreateAreaMovement(WayToPoint newWayToPoint,int speed, List<WayToPoint> wayPoint, LayerMask _layerMask)
    //{
    //    if(speed > 0)
    //    {
    //        CheckArea(newWayToPoint,speed, wayPoint, Vector3Int.down, _layerMask);
    //        CheckArea(newWayToPoint,speed, wayPoint, Vector3Int.up, _layerMask);
    //        CheckArea(newWayToPoint,speed, wayPoint, Vector3Int.left, _layerMask);
    //        CheckArea(newWayToPoint,speed, wayPoint, Vector3Int.right, _layerMask);
    //    }
    //}
    //private void CheckArea(WayToPoint WayToPoint,int speed, List<WayToPoint> area, Vector3Int offset,LayerMask _layerMask)
    //{
    //    if (WayToPoint.lastDelay == -1 * offset) return;
    //    var newPosition = newPoint(WayToPoint.lastPointToMap + offset);
    //    WayToPoint newWayToPoint = new(WayToPoint,newPosition);
    //    newWayToPoint.lastDelay = offset;
    //    if (!_tilemap.HasTile(newWayToPoint.lastPointToMap)) return;
    //    if (!CheckValidWay(WayToPoint.lastPointToWorld, newWayToPoint.lastPointToWorld, _layerMask)) return;
    //    var oldWay = area.FirstOrDefault(x => x.lastPointToMap == newWayToPoint.lastPointToMap);
    //    if (oldWay is not null)
    //        oldWay.CheckBestWay(newWayToPoint);

    //    else
    //        area.Add(newWayToPoint);
    //    CreateAreaMovement(newWayToPoint, speed - 1, area, _layerMask);
    //}
    //private bool CheckValidWay(Vector3 startPosition, Vector3 endPosition, LayerMask layerMaskObstacle)
    //{
    //    var direction = (endPosition - startPosition).normalized;
    //    var distance = Vector2.Distance(startPosition, endPosition);
    //    RaycastHit2D[] hits = Physics2D.RaycastAll(startPosition, direction, distance, layerMaskObstacle);
    //    return hits.Length == 0;
    //}

    public void UpdateStatus(StatusUnit status, UnitData unitData)
    {
        if (status == StatusUnit.Idle)
        {
            //unitData.listValidPointForMove = GetAreaMovement(unitData.transformUnit.position, unitData.MaxCellMove, unitData._layerMaskObstacle);
            unitData.Range_Area = new RangeArea(_tilemap, unitData.MaxCellMove, unitData._layerMaskObstacle);

        }
    }
}
