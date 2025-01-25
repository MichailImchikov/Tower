using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMB : MonoBehaviour
{
    protected MapManagerMB _mapManager;
    protected LayerMask _layerMaskObstacle;
    public int speed;
    private void Start()
    {
        _mapManager = FindObjectOfType<MapManagerMB>();
        _layerMaskObstacle = LayerMask.GetMask("Obstacle");
    }
}
