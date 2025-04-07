using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Configs/Ability/NewWeapon")]
public class WeaponConfig : Config
{
    [SerializeField] public List<AbilityConfig> AbilitiesConfig = new();
    public Sprite WeaponSprite;
    public Arm Arm;
    public Equipment Equipment;
    // to do ViewWeaponLogick;
}
