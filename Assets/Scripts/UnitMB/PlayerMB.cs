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
            if (RequestToMove.ContainsKey(validPoint))
            {
                unitData._pathfinder=new(unitData.Range_Area);
                unitData._pathfinder.CreatePath(transform.position, positionClick, unitData.Range_Area.Tilemap);
                SetTemperature(StatusUnit.Move);
            }
            //if (RequestToMove is not null)
            //{
            // unitData._wayToPoint = RequestToMove;
            //    SetTemperature(StatusUnit.Move);
            //}
        }
    }
}
