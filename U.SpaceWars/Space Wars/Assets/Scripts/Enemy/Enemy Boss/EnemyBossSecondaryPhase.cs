using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossSecondaryPhase : EnemyBossPrimaryPhase
{
    [SerializeField] float minLaserShotTime;
    [SerializeField] float maxLaserShotTime;
    [SerializeField] Weapon[] lasers;

    private void Start()
    {
        CalculateLaserShotDelayTime();
    }

    public override void Update()
    {
        base.Update();

        for (int i = 0; i < lasers.Length; i++)
        {
            if (lasers[i].shotDelayTime + lasers[i].lastShotTime <= Time.time)
                Fire(lasers[i]);
        }
    }

    public override void Fire(Weapon weapon)
    {
        base.Fire(weapon);
        if (weapon.GetComponent<Blaster>() != null) CalculateBlasterShotTime();
        else if (weapon.GetComponent<SpreadShot>() != null) CalculateSpreadShotTime();
        else CalculateLaserShotDelayTime();
    }

    void CalculateLaserShotDelayTime()
    {
        for (int i = 0; i < lasers.Length; i++)
        {
            float shotDelayTime = Random.Range(minLaserShotTime, maxLaserShotTime);
            lasers[i].shotDelayTime = shotDelayTime;
        }
    }

    public override void AttackAll()
    {
        base.AttackAll();

        foreach (Weapon laser in lasers)
        {
            Fire(laser);
        }

        CalculateLaserShotDelayTime();
    }

    public override void ResetShotDelayTimes()
    {
        base.ResetShotDelayTimes();

        foreach(Weapon laser in lasers)
        {
            laser.lastShotTime = 0f;
        }
    }
}
