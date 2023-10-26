using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] Image healthBarFill;

    private void Start()
    {
        EventManager.Instance.UpdateHealthBarEvent += UpdateHealthBar;

        UpdateHealthBar(PlayerPrefs.GetInt(Constants.PLAYER_HEALTH_SAVE_STRING), 
            PlayerPrefs.GetInt(Constants.PLAYER_MAX_HEALTH_SAVE_STRING));
    }

    private void OnDestroy()
    {
        EventManager.Instance.UpdateHealthBarEvent -= UpdateHealthBar;
    }

    void UpdateHealthBar(int curHealth, int maxHealth)
    {
        healthBarFill.fillAmount = (float) curHealth / maxHealth;
    }
}
