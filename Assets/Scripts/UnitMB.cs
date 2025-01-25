using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMB : MonoBehaviour
{
    protected MapManagerMB mapManager;
    protected LayerMask _layerMaskObstacle;
    private void Start()
    {
        mapManager = FindObjectOfType<MapManagerMB>();
        _layerMaskObstacle = LayerMask.GetMask("Obstacle");
    }
}
