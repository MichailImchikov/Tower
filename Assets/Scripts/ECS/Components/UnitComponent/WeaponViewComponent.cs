using System.Collections.Generic;
using UnityEngine;

namespace Client {
    struct WeaponViewComponent {
        public List<WeaponView> WeaponsView;
        public void ChangeSprite(Arm arm,Equipment equipment, Sprite sprite)
        {
            foreach(var weapon in WeaponsView)
            {
                if (arm != weapon.arm) continue;
                weapon.spriteRenderer.sprite = null;
                if(equipment == weapon.equipment)
                    weapon.spriteRenderer.sprite = sprite;
            }
        }
    }
}