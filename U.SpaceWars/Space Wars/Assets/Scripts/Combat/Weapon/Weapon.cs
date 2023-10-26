using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon: MonoBehaviour
{
    public float shotDelayTime;
    public float lastShotTime = 0f;
    public virtual bool CanFire()
    {
        if (shotDelayTime + lastShotTime <= Time.time) return true;
        else
        {
            return false;
        }
    }
    public abstract void Fire();

    public virtual void PlayAudio()
    {
        if (GetComponent<AudioSource>() != null)
        {
            EventManager.Instance.InvokePlayerWeaponFireSFXEvent();
        }
    }
}
