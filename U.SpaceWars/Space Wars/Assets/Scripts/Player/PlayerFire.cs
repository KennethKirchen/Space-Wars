using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Blaster,
    Spreadshot
}

public class PlayerFire : MonoBehaviour
{
    [SerializeField] Blaster blasterWeapon;
    [SerializeField] SpreadShot spreadShotWeapon;
    [SerializeField] WeaponType weaponType;
    Weapon activeWeapon;

    private void Start()
    {
        InputReader.Instance.FireEvent += Fire;
    }

    void Fire()
    {
        if (weaponType == WeaponType.Blaster) blasterWeapon.Fire();
        else if (weaponType == WeaponType.Spreadshot) spreadShotWeapon.Fire();
    }

    public void ChangeActiveWeapon(WeaponType weaponType)
    {
        this.weaponType = weaponType;
    }
}
