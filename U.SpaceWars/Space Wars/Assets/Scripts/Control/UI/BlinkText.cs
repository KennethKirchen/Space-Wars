using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlinkText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textToBlink;
    bool isBlinking = false;

    private void Update()
    {
        if(!isBlinking) StartBlinking();

        if (GameManager.Instance.IsPaused())
        {
            StopBlinking();
        }
    }

    IEnumerator Blink()
    {
        while (true)
        {
            switch (textToBlink.color.a.ToString())
            {
                case "0":
                    textToBlink.color = new Color(textToBlink.color.r,
                        textToBlink.color.g,
                        textToBlink.color.b, 1);

                    yield return new WaitForSeconds(0.5f);
                    break;

                case "1":
                    textToBlink.color = new Color(textToBlink.color.r,
                        textToBlink.color.g,
                        textToBlink.color.b, 0);

                    yield return new WaitForSeconds(0.5f);
                    break;
            }
        }
    }

    void StartBlinking()
    {
        if (!ShouldBlink())
        {
            StopBlinking();
            return;
        }

        StopCoroutine("Blink");
        StartCoroutine("Blink");
        isBlinking = true;

        Invoke("StopBlinking", 5f);
    }

    void StopBlinking()
    {
        StopCoroutine("Blink");
        Destroy(gameObject);
    }

    bool ShouldBlink()
    {
        int wave = GameManager.Instance.GetWave();

        if (wave == 1) return false; // Do not show text on first level.
        if (EnemyFormationActivator.Instance == null) return false;
        wave = wave % EnemyFormationActivator.Instance.GetFormationCount();
        if (wave == 1) return true; // Show text on first level after a boss wave.
        else return false;
    }
}
