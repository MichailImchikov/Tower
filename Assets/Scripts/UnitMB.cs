using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AnimatorController))]
public class UnitMB : MonoBehaviour
{
    protected MapManagerMB _mapManager;
    protected LayerMask _layerMaskObstacle;
    protected List<WayToPoint> listValidPointForMove;
    public int MaxCellMove;
    private AnimatorController _animator;
    private void Start()
    {
        _mapManager = FindObjectOfType<MapManagerMB>();
        _layerMaskObstacle = LayerMask.GetMask("Obstacle");
        listValidPointForMove = _mapManager.GetAreMovement(transform.position, MaxCellMove, _layerMaskObstacle);
        _animator = GetComponent<AnimatorController>();
    }

}
