using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] int curHealth;
    [SerializeField] FloatingHealthBar healthBar;

    public event Action<Vector3> PickupDropEvent;

    public static string PLAYER_TAG_STRING = "Player";
    public static string ENEMY_TAG_STRING = "Enemy";

    private void Awake()
    {
        if(tag == ENEMY_TAG_STRING) healthBar = GetComponentInChildren<FloatingHealthBar>();
        if(tag == PLAYER_TAG_STRING)
        {
            PlayerPrefs.SetInt("Player Max Health", maxHealth);
        }
    }

    private void Start()
    {
        if(gameObject.tag == PLAYER_TAG_STRING)
        {
            EventManager.Instance.RestoreHealthEvent += RestoreHealth;
            UpdateUI();
            curHealth = PlayerPrefs.GetInt("Player Health");

            if (curHealth == 0) curHealth = maxHealth;
        }
        if (gameObject.tag == ENEMY_TAG_STRING) healthBar.UpdateHealthbar(curHealth, maxHealth);
    }

    private void OnDestroy()
    {
        EventManager.Instance.RestoreHealthEvent -= RestoreHealth;
    }
    public void TakeDamage(int damage)
    {
        CalculateHealth(damage);
        if (gameObject.tag == PLAYER_TAG_STRING) UpdateUI();
        if (curHealth == 0) Die();
    }

    void CalculateHealth(int damage)
    {
        curHealth -= damage;
        curHealth = Mathf.Max(0, curHealth);

        if (tag == ENEMY_TAG_STRING) healthBar.UpdateHealthbar(curHealth, maxHealth);
    }

    void UpdateUI()
    {
        EventManager.Instance.InvokeUpdateHealthBarEvent(curHealth, maxHealth);
    }

    void Die()
    {
        PickupDropEvent?.Invoke(gameObject.transform.position);

        if(tag == PLAYER_TAG_STRING)
        {
            EventManager.Instance.InvokePlayerDeathEvent();
        }

        if(tag == ENEMY_TAG_STRING)
        {
            ActivateUIScore();
            Invoke("DestroyGO", 0.5f);
        }
        else DestroyGO();
    }

    void DestroyGO()
    {
        Destroy(gameObject);
    }

    void ActivateUIScore()
    {
        GetComponent<Score>().ActivateUI();
    }

    public void RestoreHealth(int restoreAmount)
    {
        curHealth += restoreAmount;
        curHealth = Mathf.Min(maxHealth, curHealth);
        EventManager.Instance.InvokeUpdateHealthBarEvent(curHealth, maxHealth);
    }

    public int GetCurrentHealth()
    {
        return curHealth;
    }

    public int GetHealth()
    {
        return curHealth;
    }

    public void SetHealth(int curHealth, int maxHealth)
    {
        this.curHealth = curHealth;
        this.maxHealth = maxHealth;
    }
}
