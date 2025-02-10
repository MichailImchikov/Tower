using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
[CreateAssetMenu(fileName = "NewAbility", menuName = "Configs/Ability/NewAbility")]
public class AbilityConfig : Config
{
    public float Damage;
    public float Cost;
    public TileBase TileView;
    [SerializeReference] AttackZoneConfig AttackZoneConfig;
}
