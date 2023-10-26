using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour, IPickup
{
    [SerializeField] int healthRestoreAmount;

    public void BeginEffect(Collider2D collision)
    {
        EventManager.Instance.InvokeRestoreHealthEvent(healthRestoreAmount);
    }

    public void CancelEffect()
    {
        return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            BeginEffect(collision);

            Destroy(gameObject);
        }
    }
}
