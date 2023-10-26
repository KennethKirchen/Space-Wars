using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreTextUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    void Start()
    {
        int beginningScore = GameManager.Instance.GetScore();
        UpdateScoreText(beginningScore);

        EventManager.Instance.UpdateScoreTextUIEvent += UpdateScoreText;
    }

    private void OnDestroy()
    {
        EventManager.Instance.UpdateScoreTextUIEvent -= UpdateScoreText;
    }

    private void UpdateScoreText(int score)
    {
        scoreText.text = score.ToString();
    }
}
