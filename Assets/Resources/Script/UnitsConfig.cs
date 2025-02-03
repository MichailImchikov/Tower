using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "UnitsConfig", menuName = "Configs/UnitsConfig")]
public class UnitsConfig : Config
{
    [SerializeReference] public List<UnitMB> units;
}
