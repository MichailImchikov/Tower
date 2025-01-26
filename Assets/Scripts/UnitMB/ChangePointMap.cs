using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePointMap : MonoBehaviour, IObserverAction
{
    private Transform followPoint1;
    private Transform followPoint2;
    private Transform unitTransform;
    private bool isFollow;
    private void Awake()
    {
        followPoint1 = transform.GetChild(0);
        followPoint2 = transform.GetChild(1);
    }
    private void Update()
    {
        if (!isFollow) return;
        followPoint1.position = unitTransform.position;
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var validPoint = MapManagerMB.Instance.generalTileMap.newPoint(mousePosition);
        followPoint2.position = validPoint.PointToWorld;

    }
    public void UpdateStatus(StatusUnit status, UnitData unitData)
    {
        if (status == StatusUnit.Idle)
        {
            isFollow = true;
            unitTransform = unitData.transformUnit;
            followPoint1.gameObject.SetActive(true);
            followPoint2.gameObject.SetActive(true);
        }
        else
        {
            isFollow = false;
            followPoint1.gameObject.SetActive(false);
            followPoint2.gameObject.SetActive(false);
        }

    }
}
