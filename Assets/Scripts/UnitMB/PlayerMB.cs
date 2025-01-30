using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PlayerMB : UnitMB
{
    public void EndMove()
    {
        SetTemperature(StatusUnit.Idle);
    }
    public override void Init()
    {
        RegisterObserver(FindObjectOfType<ChangePointMap>());
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && statusUnit != StatusUnit.Move)
        {
            var positionClick = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            var validPoint = GeneralTileMap.Instance.newPoint(positionClick);
            var RequestToMove = unitData.Range_Area.Area;
            if (RequestToMove.ContainsKey(validPoint)&& !new PointMap(positionClick, unitData.Range_Area.Tilemap).Equals(new PointMap(unitData.transformUnit.position, unitData.Range_Area.Tilemap)))
            {
                unitData._pathfinder=new(unitData.Range_Area);
                unitData._pathfinder.CreatePath(positionClick, unitData);
                SetTemperature(StatusUnit.Move);
            }
        }
    }
}
