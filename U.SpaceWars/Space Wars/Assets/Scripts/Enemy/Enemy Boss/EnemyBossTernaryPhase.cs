using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossTernaryPhase : EnemyBossSecondaryPhase
{
    [SerializeField] float attackAllDelay;
    float lastAttackAllTime = 0;

    private void OnEnable()
    {
        AttackAll();
    }

    public override void Update()
    {
        base.Update();

        if (attackAllDelay + lastAttackAllTime <= Time.time)
            AttackAll();
    }

    public override void AttackAll()
    {
        base.AttackAll();
        lastAttackAllTime = Time.time;
    }
}
