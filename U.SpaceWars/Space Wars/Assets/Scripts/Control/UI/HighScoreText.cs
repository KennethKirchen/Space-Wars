using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highScoreText;
    void Awake()
    {
        highScoreText.text = PlayerPrefs.GetInt(Constants.HIGH_SCORE_SAVE_STRING).ToString();
    }
}
