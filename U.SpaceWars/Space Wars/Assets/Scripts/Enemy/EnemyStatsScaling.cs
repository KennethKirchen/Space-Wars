using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsScaling : MonoBehaviour
{
    [SerializeField] float healthExponentScalingAmount;
    [SerializeField] float damageScalingAmount;
    float health;

    private void Awake()
    {
        health = GetComponent<Health>().GetHealth();
    }

    private void Start()
    {
        int wave = GameManager.Instance.GetWave();
        health = health * Mathf.Pow(wave, healthExponentScalingAmount);
        int intHealth = Mathf.FloorToInt(health);
        GetComponent<Health>().SetHealth(intHealth, intHealth);
    }
}
