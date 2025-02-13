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
    private void Move(bool flag)
    {
        _animator.SetBool("Move", flag);
    }
}

