using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[System.Serializable]
public class Ability
{
    public float Damage;
    public int Cost;
    public TileBase TileView;
    [SerializeReference] public AttackZoneConfig AttackZoneConfig;
}
