using System.Collections;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

public class GoalkeeperManager : MonoBehaviour
{
    public Animator goalkeeperAC;
    public Animator ballAC;
    public Animator buttons01AC;
    public Animator buttons02AC;
    public Animator buttons03AC;
    public Animator buttons04AC;
    public Image[] answerFeedback;
    public Sprite[] feedbackSprite;

    public int answerIndex;
    public static int correctAnswer;
    public static bool cleared;


    private EventInstance cryEvent;
    private EventInstance laughtEvent;
    private EventInstance prepareEvent;


    private void Awake()
    {
        cryEvent = RuntimeManager.CreateInstance(StaticFmodRefs.SoccerRefs.PATH_BOSS_CRY);
        laughtEvent = RuntimeManager.CreateInstance(StaticFmodRefs.SoccerRefs.PATH_BOSS_LAUGH);
        prepareEvent = RuntimeManager.CreateInstance(StaticFmodRefs.SoccerRefs.PATH_BOSS_PREPARE);
    }

    private void Update()
    {
        if (SoccerChallenge.blocked == true)
        {
            //WrongAnswer();
            StartCoroutine("Wrong");
        }

        else
        {
            StopCoroutine("Wrong");
        }

        if (SoccerChallenge.floped == true)
        {
            //CorrectAnswer();
            StartCoroutine("Correct");
        }

        else
        {
            StopCoroutine("Correct");
        }

        if (SoccerManager.clearIndexAnswer == true)
        {
            Debug.Log("Limpou");
            ClearIndex();
        }

    }

    private IEnumerator Correct()
    {
        yield return new WaitForSeconds(0.5f);

        ballAC.SetBool("Goal", true);
        goalkeeperAC.SetTrigger("Floped");
        buttons01AC.SetBool("Goal", true);
        buttons02AC.SetBool("Goal", true);
        buttons03AC.SetBool("Goal", true);
        buttons04AC.SetBool("Goal", true);
        answerFeedback[SoccerManager.Instance.CurrentLevel.CurrentChallengeIndex].sprite = feedbackSprite[0];
        //Debug.Log("verde");
        answerIndex++;
        correctAnswer++;
        SoccerChallenge.floped = false;

    }

    private IEnumerator Wrong()
    {
        yield return new WaitForSeconds(0.2f);

        ballAC.SetBool("Goal", false);
        goalkeeperAC.SetTrigger("Blocked");
        buttons01AC.SetBool("Goal", false);
        buttons02AC.SetBool("Goal", false);
        buttons03AC.SetBool("Goal", false);
        buttons04AC.SetBool("Goal", false);
        answerFeedback[SoccerManager.Instance.CurrentLevel.CurrentChallengeIndex].sprite = feedbackSprite[1];
        answerIndex++;
        SoccerChallenge.blocked = false;
    }


    /*private void CorrectAnswer()
    {
        ballAC.SetBool("Goal", true);
        goalkeeperAC.SetTrigger("Floped");
        buttons01AC.SetBool("Goal", true);
        buttons02AC.SetBool("Goal", true);
        buttons03AC.SetBool("Goal", true);
        buttons04AC.SetBool("Goal", true);
        answerFeedback[SoccerManager.Instance.CurrentLevel.CurrentChallengeIndex].sprite = feedbackSprite[0];
        answerIndex++;
        correctAnswer++;
        SoccerChallenge.floped = false;
    }

    private void WrongAnswer()
    {
        ballAC.SetBool("Goal", false);
        Debug.Log("SetBoolGoal");
        goalkeeperAC.SetTrigger("Blocked");
        buttons01AC.SetBool("Goal", false);
        buttons02AC.SetBool("Goal", false);
        buttons03AC.SetBool("Goal", false);
        buttons04AC.SetBool("Goal", false);
        answerFeedback[answerIndex].sprite = feedbackSprite[1];
        answerIndex++;
        SoccerChallenge.blocked = false;
    }*/


    private void ClearIndex()
    {
        ScoreDisplay.Instance.DisplayScore(SoccerManager.Instance.CurrentLevel);
        cleared = true;
        answerIndex = 0;

        for (int i = 0; i < answerFeedback.Length; i++)
        {
            answerFeedback[i].sprite = null;
        }

        SoccerManager.clearIndexAnswer = false;
        cleared = false;
        //Debug.Log("limpou");
    }


    //animation Events
    public void Cry()
    {
        Debug.Log("Boss CRY");
        cryEvent.start();
    }
    public void Laugh()
    {
        Debug.Log("Boss LAUGH");

        laughtEvent.start();
    }
    public void Prepare()
    {
        Debug.Log("Boss PREPARE");

        prepareEvent.start();
    }
}
