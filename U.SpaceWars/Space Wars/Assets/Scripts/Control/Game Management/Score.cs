using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] int scoreToGive;
    [SerializeField] GameObject scoreUIObject;
    [SerializeField] float upwardForce;
    [SerializeField] GameObject healthBar;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer[] weaponArtToDisable;

    const string ENEMY_TAG_STRING = "Enemy";

    private void OnDestroy()
    {
        EventManager.Instance.InvokeAwardPointsEvent(scoreToGive);

        if(tag == ENEMY_TAG_STRING)
        {
            EventManager.Instance.InvokeReduceEnemyCountEvent();
        }
    }

    public void ActivateUI()
    {
        scoreUIObject.SetActive(true);
        healthBar.SetActive(false);
        spriteRenderer.enabled = false;

        scoreUIObject.GetComponent<TextMeshProUGUI>().text = scoreToGive.ToString();
        scoreUIObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, upwardForce), ForceMode2D.Impulse);

        for(int i = 0; i < weaponArtToDisable.Length; i++)
        {
            weaponArtToDisable[i].enabled = false;
        }
    }
}
