using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[System.Serializable]
public class Ability
{
    public float Damage;
    public float Cost;
    public TileBase TileView;
    [SerializeReference] public AttackZoneConfig AttackZoneConfig;
}
