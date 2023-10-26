using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public event Action<int> RestoreHealthEvent;
    public event Action PowerupEvent;
    public event Action<int> AwardPointsEvent;
    public event Action<int, int> UpdateHealthBarEvent;
    public event Action<int> UpdateScoreTextUIEvent;
    public event Action PlayerDeathEvent;
    public event Action PlayerWeaponFireSFXEvent;
    public event Action ChangeSFXVolumeEvent;
    public event Action ChangeMusicVolumeEvent;
    public event Action InvertControlsEvent;
    public event Action ReduceEnemyCountEvent;

    private void Awake()
    {
        Instance = this;
    }
    public void InvokeRestoreHealthEvent(int restoreAmount)
    {
        RestoreHealthEvent?.Invoke(restoreAmount);
    }

    public void InvokePowerupEvent()
    {
        PowerupEvent?.Invoke();
    }

    public void InvokeAwardPointsEvent(int scoreToGive)
    {
        AwardPointsEvent?.Invoke(scoreToGive);
    }

    public void InvokeUpdateHealthBarEvent(int curHealth, int maxHealth)
    {
        UpdateHealthBarEvent?.Invoke(curHealth, maxHealth);
    }

    public void InvokeUpdateScoreTextUIEvent(int score)
    {
        UpdateScoreTextUIEvent?.Invoke(score);
    }

    public void InvokePlayerDeathEvent()
    {
        PlayerDeathEvent?.Invoke();
    }

    public void InvokePlayerWeaponFireSFXEvent()
    {
        PlayerWeaponFireSFXEvent?.Invoke();
    }

    public void InvokeChangeSFXVolumeEvent()
    {
        ChangeSFXVolumeEvent?.Invoke();
    }

    public void InvokeChangeMusicVolumeEvent()
    {
        ChangeMusicVolumeEvent?.Invoke();
    }

    public void InvokeInvertControlsEvent()
    {
        InvertControlsEvent?.Invoke();
    }

    public void InvokeReduceEnemyCountEvent()
    {
        ReduceEnemyCountEvent?.Invoke();
    }
}
