using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PlayerMB : UnitMB
{
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var positionClick = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var validPoint = GeneralTileMap.newPoint(positionClick);
            if(listValidPointForMove.Any(x => validPoint.PointToMap ==x.lastPointToMap))
            {
                listValidPointForMove = _mapManager.GetAreMovement(validPoint.PointToWorld, MaxCellMove, _layerMaskObstacle);
                transform.position = validPoint.PointToWorld;
            }
        }
    }
}
