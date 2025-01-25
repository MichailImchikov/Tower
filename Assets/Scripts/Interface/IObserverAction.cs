using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserverAction
{
    public void UpdateStatus(StatusUnit status, UnitData unitData);
}
public enum StatusUnit
{
    Idle,
    Move,
    Dead,
    DeBuff,
    Attack,
    Damage
}