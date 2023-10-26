using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Weapon
{
    [SerializeField] GameObject LaserPF;
    [SerializeField] float laserLifeTime;
    public override void Fire()
    {
        if (!CanFire()) return;
        LaserPF.SetActive(true);
        Invoke("CancelFire", laserLifeTime);
    }

    void CancelFire() 
    {
        LaserPF.SetActive(false);
        lastShotTime = Time.time;
    }
}
