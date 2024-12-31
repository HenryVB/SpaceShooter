using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameScore : MonoBehaviour
{
    private TextMeshProUGUI textScore;
    private int score;

    public int Score { get => score; set { score = value; UpdateScoreText(); }  }

    // Start is called before the first frame update
    void Start()
    {
        textScore = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateScoreText()
    {
        string scoreformatted = string.Format("{0:0000000}", score);
        textScore.text = scoreformatted;
    }
}
