using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadShot : Weapon
{
    [SerializeField] GameObject[] bulletPFs;
    [SerializeField] float startingAngleOffset;
    [SerializeField] float angleOffsetAmount;
    float currentAngleOffset;

    public override void Fire()
    {
        if (!CanFire()) return;

        currentAngleOffset = startingAngleOffset;
        PlayAudio();

        for (int i = 0; i < bulletPFs.Length; i++)
        {
            bulletPFs[i].GetComponent<Bullet>().SetAngleOffset(currentAngleOffset);
            currentAngleOffset += angleOffsetAmount;
            Instantiate(bulletPFs[i], gameObject.transform);
        }

        lastShotTime = Time.time;
    }
}
