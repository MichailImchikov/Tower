using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour, IObserverAction
{
    private Animator _animator;
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    private void Move(bool flag)
    {
        _animator.SetBool("Move", flag);
    }
    private void SetTrigger(StatusUnit state)
    {
        _animator.SetTrigger($"{state.ToString()}");
    }
    public void UpdateStatus(StatusUnit status, UnitData unitData)
    {
        if (status == StatusUnit.Idle)
        {
            Move(false);
            return;
        }
        if (status == StatusUnit.Attack)
        {
            Move(true);
            return;
        }
        SetTrigger(status);
    }
}

