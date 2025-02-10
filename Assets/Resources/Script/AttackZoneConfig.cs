using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewZone", menuName = "Configs/Ability/NewZone")]
public class AttackZoneConfig : ScriptableObject
{
    public AttackZone attackZone = new();
}