using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Weapon weapon;
    [SerializeField] float minShotDelayTime;
    [SerializeField] float maxShotDelayTime;
    float lastShotTime = 0f;
    float shotDelayTime;
    bool hasSpawnedPickup = false;

    [SerializeField] GameObject[] pickupsToDrop;

    private void Start()
    {
        CalculateShotDelayTime();

        GetComponent<Health>().PickupDropEvent += SpawnPickup;
    }

    private void OnDestroy()
    {
        GetComponent<Health>().PickupDropEvent -= SpawnPickup;
    }

    private void Update()
    {
        if(shotDelayTime + lastShotTime < Time.time)
        {
            Fire();
        }
    }


    void Fire()
    {
        if(weapon != null) weapon.GetComponent<Weapon>().Fire();
        lastShotTime = Time.time;
        CalculateShotDelayTime();
    }

    void CalculateShotDelayTime()
    {
        if (weapon == null) return;

        shotDelayTime = Random.Range(minShotDelayTime, maxShotDelayTime);
        GetComponentInChildren<Weapon>().shotDelayTime = shotDelayTime;
    }

    void SpawnPickup(Vector3 spawnPos)
    {
        if (hasSpawnedPickup) return;

        int index = Random.Range(0, pickupsToDrop.Length - 1);
        if(pickupsToDrop[index] != null)
        {
            Instantiate(pickupsToDrop[index], spawnPos, Quaternion.identity);
        }

        hasSpawnedPickup = true;
    }
}
