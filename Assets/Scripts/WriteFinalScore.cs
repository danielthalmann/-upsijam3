using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WriteFinalScore : MonoBehaviour
{
	public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        if (ScoreCount.Instance != null)
           scoreText.SetText("Score: "+ScoreCount.Instance.GetScoreCount().ToString());
    }
}
