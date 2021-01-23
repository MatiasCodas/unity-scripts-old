using FMOD.Studio;
using FMODUnity;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreCountdown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelScoreText;
    [SerializeField] private TextMeshProUGUI totalScoreText;

    [SerializeField] private float cascateTickTime = 0.1f;
    private WaitForSeconds cascateTickWait;

    private int cascatingScore;
    private int totalScore;

    [SerializeField] [EventRef] private string addScorePath;
    private EventInstance addScoreEvent;

    [SerializeField] private UnityEvent OnCascateEnd;

    private void Awake()
    {
        cascateTickWait = new WaitForSeconds(cascateTickTime);
        addScoreEvent = RuntimeManager.CreateInstance(addScorePath);
    }

    //invoked via UnityEvent
    public void StartScoreCascate()
    {
        //Debug.Log("CASCATE BREAK");
        //Debug.Break();

        StartCoroutine(Cascate());
    }

    private IEnumerator Cascate()
    {
        Debug.Log("Started Cascating");
        //Debug.Break();

        cascatingScore = SoccerManager.Instance.CurrentLevel.PointsReceived;
        totalScore = SoccerManager.Instance.totalPoints;

        while (cascatingScore > 0)
        {
            cascatingScore--;
            totalScore++;

            UpdateDisplay();
            addScoreEvent.start();
            yield return cascateTickWait;
        }
        SoccerManager.Instance.totalPoints = totalScore;
        OnCascateEnd?.Invoke();
    }

    private void UpdateDisplay()
    {
        totalScoreText.text = totalScore.ToString();
        levelScoreText.text = cascatingScore.ToString();
    }

}
