using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
[RequireComponent(typeof(AnimatorController))]
public abstract class UnitMB : MonoBehaviour
{
    [SerializeField] protected UnitData unitData = new();

    protected List<IObserverAction> listObserversAction = new();
    protected StatusUnit statusUnit;
    private void Start()
    {
        unitData._layerMaskObstacle = LayerMask.GetMask("Obstacle");
        unitData._generalTileMap = FindObjectOfType<GeneralTileMap>();
        RegisterObserver(unitData._generalTileMap);
        RegisterObserver(FindAnyObjectByType<MapAreaMovement>());
        RegisterObserver(GetComponent<AnimatorController>());
        RegisterObserver(GetComponent<UnitMoveMB>());
        unitData.transformUnit = transform;
        Init();
        SetTemperature(StatusUnit.Idle);
    }
    public abstract void Init();
    public void RegisterObserver(IObserverAction observer)
    {
        listObserversAction.Add(observer);
    }
    public void RemoveObserver(IObserverAction observer)
    {
        listObserversAction.Remove(observer);
    }
    public void SetTemperature(StatusUnit temp)
    {
        statusUnit = temp;
        NotifyObservers();
    }
    private void NotifyObservers()
    {
        foreach (var observer in listObserversAction)
        {
            observer.UpdateStatus(statusUnit,unitData);
        }
    }
}
[Serializable]
public class UnitData
{
    [HideInInspector] public Transform transformUnit;
    [HideInInspector] public LayerMask _layerMaskObstacle;
    [HideInInspector] public Pathfinder _pathfinder;
    [HideInInspector] public RangeArea Range_Area;
    [HideInInspector] public GeneralTileMap _generalTileMap;
    [HideInInspector] public PointMap _pointMap;
    public int MaxCellMove = 3;
    public float Speed = 1;
}
