using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] 
    TMP_Text Player1Score;

    [SerializeField]
    TMP_Text Player2Score;

    [SerializeField]
    BallManager ball;


    void Start()
    {
        Player1Score.text = $"Score Player1:\n{ball.ScorePlayer1}";
        Player2Score.text = $"Score Player2:\n{ball.ScorePlayer2}";
    }

    void Update()
    {
        Player1Score.text = $"Score Player1:\n{ball.ScorePlayer1}";
        Player2Score.text = $"Score Player2:\n{ball.ScorePlayer2}";
    }
}
