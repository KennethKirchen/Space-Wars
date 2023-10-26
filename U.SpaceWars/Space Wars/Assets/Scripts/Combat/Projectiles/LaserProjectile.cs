using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    enum Target
    {
        Player,
        Enemy
    }

    [SerializeField] Target target;
    [SerializeField] float damageinterval;
    [SerializeField] int damage;
    float lastDamageInterval = 0;
    bool isInside = false;
    Collider2D collision;

    private void Update()
    {
        if (CheckDamageInterval() && isInside) DealDamage();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == target.ToString())
        {
            isInside = true;
            this.collision = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == target.ToString())
        {
            isInside = false;
        }
    }

    bool CheckDamageInterval()
    {
        if (Time.time > damageinterval + lastDamageInterval)
        {
            return true;
        }
        else return false;
    }

    void DealDamage()
    {
        if (collision.GetComponent<Health>() == null) return;

        collision.GetComponent<Health>().TakeDamage(damage);
        lastDamageInterval = Time.time;
    }
}
