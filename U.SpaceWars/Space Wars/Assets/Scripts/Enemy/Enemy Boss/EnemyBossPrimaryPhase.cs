using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossPrimaryPhase : EnemyBoss
{
    [SerializeField] float minBlasterShotDelayTime;
    [SerializeField] float maxBlasterShotDelayTime;
    [SerializeField] Weapon[] blasters;
    [SerializeField] float minSpreadShotDelayTime;
    [SerializeField] float maxSpreadShotDelayTime;
    [SerializeField] Weapon spreadShot;

    private void Start()
    {
        CalculateBlasterShotTime();
        CalculateSpreadShotTime();
    }

    public override void Update()
    {
        base.Update();

        for (int i = 0; i < blasters.Length; i++)
        {
            if (blasters[i].shotDelayTime + blasters[i].lastShotTime <= Time.time)
                Fire(blasters[i]);
        }

        if(spreadShot.shotDelayTime + spreadShot.lastShotTime <= Time.time)
        {
            Fire(spreadShot);
        }
    }

    public override void Fire(Weapon weapon)
    {
        base.Fire(weapon);
        if(weapon.GetComponent<SpreadShot>() == null) CalculateBlasterShotTime();
        else CalculateSpreadShotTime();
    }

    public void CalculateBlasterShotTime()
    {
        for(int i = 0; i < blasters.Length; i++)
        {
            float shotDelayTime = Random.Range(minBlasterShotDelayTime, maxBlasterShotDelayTime);
            blasters[i].shotDelayTime = shotDelayTime;
        }
    }

    public void CalculateSpreadShotTime()
    {
        float shotDelayTime = Random.Range(minSpreadShotDelayTime, maxSpreadShotDelayTime);
        spreadShot.shotDelayTime = shotDelayTime;
    }

    public virtual void AttackAll()
    {
        ResetShotDelayTimes();
        
        foreach(Weapon blaster in blasters)
        {
            Fire(blaster);
        }

        Fire(spreadShot);

        CalculateBlasterShotTime();
        CalculateSpreadShotTime();
    }

    public virtual void ResetShotDelayTimes()
    {
        foreach(Weapon blaster in blasters)
        {
            blaster.shotDelayTime = 0f;
        }

        spreadShot.shotDelayTime = 0f;
    }
}
