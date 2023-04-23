using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCount : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public static ScoreCount Instance;

    public float scoreMultiplier = 1;
    public int secondsToMultiplier = 15;
    private int scoreCount = 0;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.SetText(scoreCount.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        float currentScore = scoreCount;
        float currentSeconds = GetCurrentSeconds();
        float currentScoreMultiplier = scoreMultiplier;

        if (currentSeconds > secondsToMultiplier)
        {
            currentScoreMultiplier = GetMultiplier();
        }

        float score = currentScoreMultiplier * currentSeconds;

        scoreCount = Mathf.RoundToInt(score);

        scoreText.SetText(scoreCount.ToString());
    }

    public float GetCurrentSeconds()
    {
        return Time.timeSinceLevelLoad;
    }

    public float GetMultiplier()
    {
        float currentSeconds = GetCurrentSeconds();
        return scoreMultiplier + (currentSeconds / secondsToMultiplier);
    }

    public int GetScoreCount()
    {
        return scoreCount;
    }
}
