using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
   
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float scoreMultiplier;

    private float score;
    private bool shouldCountScore = true;


    void Update()
    {
        if(!shouldCountScore)
        {
            return;
        }
        score += Time.deltaTime * scoreMultiplier;
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    public int StopCountScore()
    {
        shouldCountScore = false;
        scoreText.text = string.Empty;
        return Mathf.FloorToInt(score);
    }

    public void StartTimer()
    {
        shouldCountScore = true;
    }
}
