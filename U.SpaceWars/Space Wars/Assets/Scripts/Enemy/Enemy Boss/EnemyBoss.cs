using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBoss : MonoBehaviour
{
    public float healthDeactivationThreshold;
    [SerializeField] EnemyBoss nextPhase;
    [SerializeField] SpriteRenderer spriteRenderer;

    public virtual void Update()
    {
        if(GetComponent<Health>().GetCurrentHealth() <= 
            healthDeactivationThreshold && nextPhase != null)
        {
            nextPhase.enabled = true;
            enabled = false;
        }

        if (GetComponent<Health>().GetCurrentHealth() <= 0)
        {
            spriteRenderer.enabled = false;
        }
    }

    public virtual void Fire(Weapon weapon)
    {
        weapon.Fire();
        weapon.lastShotTime = Time.time;
    }
}
