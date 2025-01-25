using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator _animator;
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    public void Move(bool flag)
    {
        _animator.SetBool("Move", flag);
    }
    public void SetTrigger(AnimationStateBool state)
    {
        _animator.SetTrigger($"{state.ToString()}");
    }
}
public enum AnimationStateBool
{
    Move,
    Dead,
    DeBuff,
    Attack,
    Damage
}
