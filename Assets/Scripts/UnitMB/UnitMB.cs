using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
public class UnitMB : MonoBehaviour
{
    [HideInInspector] public int Entity;
    public int MaxCellMove = 5;
    public float Health = 100;
    public AbilityConfig abilityConfig;
    private void Start()
    {

    }
}
