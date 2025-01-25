using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMB : UnitMB
{
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var positionClick = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var centerTile = mapManager.GetCenterGeneralTile(positionClick);
            transform.position = centerTile;
        }
    }
}
