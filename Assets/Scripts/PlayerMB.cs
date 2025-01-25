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
            var centerTile = mapManager.GetCenterGeneralTile(positionClick);
            var direction = (centerTile - transform.position).normalized;
            var distance = Vector2.Distance(transform.position, centerTile);
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, distance, _layerMaskObstacle);
            if (hits.Length == 0)
            transform.position = centerTile;
        }
    }
}
