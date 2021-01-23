using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI scoreGraphText;

    [SerializeField] private TextMeshProUGUI totalScoreText;

    public int score;
    public int totalScore;
    public float timeLeft;
    private float maxPoints = 100;
    private float maxTime = 120;
    private float timeRatio;
    private float scoreTime;
    public int value;
    public int timeValue;
    public int partialValue;


    [SerializeField] private DOTweenUIMovement inTween;
    [SerializeField] private DOTweenUIMovement outTween;

    public static ScoreDisplay Instance;
    private void Awake()
    {
        #region Singleton

        if (Instance != null)
        {
            Debug.Log($"There was a {typeof(ChallengeDisplay)} already, so the {name} object was destroyed");
            Destroy(this);
            return;
        }

        Instance = this;

        #endregion 
    }



    public void DisplayScore(SoccerLevel level)
    {
        UpdateScore(level);
    }

    public void DisplayPartialScore(SoccerLevel level)
    {
        UptadePartialScore(level);
    }

    public void DisplayTotalScore(SoccerLevel level)
    {
        UpdateTotalScore(level);
    }

    public void TimeScore()
    {
        timeLeft = Countdown.Instance.RemainingTime;

        timeRatio = timeLeft / maxTime;

        scoreTime = (maxPoints * timeRatio);

        value = Mathf.RoundToInt(scoreTime);

    }

    public void UpdateScore(SoccerLevel level)
    {
        score = level.PointsReceived;

        string newText = score.ToString();

        scoreText.text = newText;



    }

    public void UptadePartialScore(SoccerLevel level)
    {
        TimeScore();
        level.PointsReceived += value;

        //score = level.PointsReceived;

        partialValue = level.PointsReceived;
        //SoccerManager.Instance.totalPoints += value;
        string newText = partialValue.ToString();
        scoreGraphText.text = newText;


    }

    public void UpdateTotalScore(SoccerLevel level)
    {
        totalScore = SoccerManager.Instance.totalPoints;

        string newText = totalScore.ToString();

        totalScoreText.text = newText;
    }


    public void TweenIn()
    {
        inTween.Play();
    }

    public void TweenOut()
    {
        outTween.Play();
    }

}
