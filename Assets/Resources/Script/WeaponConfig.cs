using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Configs/Ability/NewWeapon")]
public class WeaponConfig : Config
{
    [SerializeField] public List<AbilityConfig> AbilitiesConfig = new();
    // to do ViewWeaponLogick;
}
