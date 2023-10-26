using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : Weapon
{
    [SerializeField] GameObject bulletPF;
    float angleOffset = 90;

    public override void Fire()
    {
        if (!CanFire()) return;

        PlayAudio();
        bulletPF.GetComponent<Bullet>().SetAngleOffset(angleOffset);
        Instantiate(bulletPF, gameObject.transform);
        lastShotTime = Time.time;
    }
}
