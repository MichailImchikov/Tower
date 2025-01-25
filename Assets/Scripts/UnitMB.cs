using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMB : MonoBehaviour
{
    protected MapManager mapManager;
    private void Start()
    {
        mapManager = FindObjectOfType<MapManager>();
    }
}
