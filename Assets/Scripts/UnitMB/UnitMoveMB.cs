using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class UnitMoveMB : MonoBehaviour/*, IObserverAction*/
{
    private bool isMoving;
    private List<PointMap> targetWay;
    private int currentIndexPoint;
    private PointMap nextPoint;
    private float Speed;
    private void Update()
    {
        if (isMoving)
        {
            if (Vector3.Distance(transform.position, nextPoint.PointToWorld) > 0.001f)
                transform.position = Vector3.MoveTowards(transform.position, nextPoint.PointToWorld, Speed * Time.deltaTime);
            else
            {
                if (currentIndexPoint <= targetWay.Count - 1)
                {
                    nextPoint = targetWay[currentIndexPoint];
                    var diraction = nextPoint.PointToWorld - transform.position;
                    if (diraction.x > 0) transform.GetChild(0).localScale = new Vector3(-1, 1, 1);// to do normal, igor bi volosi na gope rval za takoe
                    else transform.GetChild(0).localScale = new Vector3(1, 1, 1);
                    currentIndexPoint++;
                }
                else
                {
                    EndMove();
                }
            }
        }
    }
    private void EndMove()
    {
        GetComponent<PlayerMB>().EndMove();
        isMoving = false;
    }

    //public void UpdateStatus(StatusUnit status, UnitData unitData)
    //{
    //    if (status == StatusUnit.Move)
    //    {
    //        Speed = unitData.Speed;
    //        targetWay = unitData._pathfinder.pointMaps;
    //        nextPoint = targetWay[0];
    //        currentIndexPoint = 0;   
    //        isMoving = true;   
    //        var diraction = nextPoint.PointToWorld - transform.position;   
    //        if (diraction.x > 0) transform.GetChild(0).localScale = new Vector3(-1, 1, 1);// to do normal, igor bi volosi na gope rval za takoe  
    //        else transform.GetChild(0).localScale = new Vector3(1, 1, 1);
    //    }
    //}
}
