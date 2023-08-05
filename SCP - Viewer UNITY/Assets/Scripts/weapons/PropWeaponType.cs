// Ётот скрипт должен быть на оружии.
//  огда у человека есть в руках оружие, то он провер€ет у последнего наличие скрипта
// и свер€ет его WhatWeaponIs.
// „еловек выбирает анимацию стрельбы в зависимости от мода WhatWeaponIs на оружии.

using UnityEngine;
public class PropWeaponType : MonoBehaviour
{
    public enum             WhatWeaponType { pistol, shotgun, automatic_rifle };
    public WhatWeaponType   weaponType;

    public static ushort FindPatrons(in WhatWeaponType weaponType)
    {     
        switch (weaponType)
        {
            case PropWeaponType.WhatWeaponType.pistol:
                return (ushort) Random.Range(8, 16);
            case PropWeaponType.WhatWeaponType.shotgun:
                return (ushort) Random.Range(4, 8);
            case PropWeaponType.WhatWeaponType.automatic_rifle:
                return (ushort) Random.Range(32, 64);
            default:
                return (ushort) Random.Range(1, 2);
        }
    }
}
