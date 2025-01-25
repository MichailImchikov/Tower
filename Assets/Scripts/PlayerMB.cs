using System.Collections;
using System.Collections.Generic;
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
            var validPoint = _mapManager.CheckValidPoint(positionClick, listValidPointForMove);
            if(validPoint.HasValue)
            {
                listValidPointForMove = _mapManager.GetAreMovment(validPoint.Value, speed, _layerMaskObstacle);
                transform.position = validPoint.Value;
            }

        }
    }
}
