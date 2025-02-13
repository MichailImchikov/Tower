using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponView : MonoBehaviour
{
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public Arm arm;
    public Equipment equipment;
    [HideInInspector] public SpriteRenderer spriteRenderer;
}
public enum Arm
{
    Right,
    Left,
}
public enum Equipment
{
    Weapon,
    Shield
}