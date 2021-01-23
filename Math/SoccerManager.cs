using UnityEngine;
using UnityEngine.Events;

public class SoccerManager : GameManager
{
    public new static SoccerManager Instance => GameManager.Instance as SoccerManager;

    public SoccerLevel[] Levels;
    public SoccerLevel CurrentLevel => Levels[CurrentLevelIndex];
    public int CurrentLevelIndex = 0;
    public SoccerLevel LastLevel => Levels[Levels.Length - 1];
    public SoccerChallenge CurrentChallenge => CurrentLevel.CurrentChallenge;

    [SerializeField] private UnityEvent OnLevelStart;
    [SerializeField] private UnityEvent OnLevelEnd;

    [SerializeField] public int MaxPoints;
    internal static bool endedLevel;
    internal static bool startLevel;
    internal static bool clearIndexAnswer;
    internal static bool endedGame;

    public int totalPoints;

    public GameObject nextLevelButton;
    public GameObject retryLevelButton;
    public GameObject retryNormalButton;

    public override void SetPauseState(bool pauseState)
    {
        base.SetPauseState(pauseState);
        Countdown.Instance.Counting = !pauseState;
    }

    protected override void Awake()
    {
        base.Awake();
        Levels = GetLevels();
    }
    

    private SoccerLevel[] GetLevels()
    {
        //todo: make this customization into a tool/custom inspector
        SoccerLevel[] newLevels = new SoccerLevel[5];

        #region Create possible Challenges
        ChallengeType[] possibleFirstLevel = new ChallengeType[1];
        possibleFirstLevel[0] = ChallengeType.Golden;

        ChallengeType[] possibleSecondLevel = new ChallengeType[1];
        possibleSecondLevel[0] = ChallengeType.Unit;

        ChallengeType[] possibleThirdLevel = new ChallengeType[2];
        possibleThirdLevel[0] = ChallengeType.Ten;
        possibleThirdLevel[1] = ChallengeType.TenSpare;

        ChallengeType[] possibleFourthLevel = new ChallengeType[1];
        possibleFourthLevel[0] = ChallengeType.UnitSub;

        //teste
        ChallengeType[] possibleFifthLevel = new ChallengeType[2];
        possibleFifthLevel[0] = ChallengeType.TenSub;
        possibleFifthLevel[1] = ChallengeType.TenSubSpare;

        ChallengeType[] possibleSixthLevel = new ChallengeType[2];
        possibleSixthLevel[0] = ChallengeType.TenSpare;
        possibleSixthLevel[1] = ChallengeType.TenSubSpare;

        #endregion

        #region Create Possible Levels

        newLevels[0] = new SoccerLevel(possibleFirstLevel, 10);
        newLevels[1] = new SoccerLevel(possibleSecondLevel, 10);
        newLevels[2] = new SoccerLevel(possibleThirdLevel, 10);
        newLevels[3] = new SoccerLevel(possibleFourthLevel, 10);
        newLevels[4] = new SoccerLevel(possibleFifthLevel, 10);
        //newLevels[5] = new SoccerLevel(possibleFourthLevel, 10);

        #endregion


        return newLevels;
    }

    //Invoked by choosing an answer
    public void NextChallenge()
    {

        BadgeDisplay.Instance.ResetBigBadge();
        CurrentLevel.PointsReceived += CurrentChallenge.PointsReceived;


        if (CurrentChallenge.Equals(CurrentLevel.LastChallenge))
        {
            Debug.Log($"{CurrentChallenge} is the last challenge in {CurrentLevel}. Ending Current Level");
            CurrentChallenge.PointsReceived = 0;
            endedLevel = true;
            EndLevel();
        }
        else
        {
            //endedGame = false;
            CurrentChallenge.PointsReceived = 0;

            //display previous challenge's equation to player
            CountDisplay.Instance.DisplayCount(CurrentChallenge);

            //update challenge
            CurrentLevel.CurrentChallengeIndex++;

            //display updated challenge
           // ChallengeDisplay.Instance.DisplayChallenge(CurrentChallenge);
            ScoreDisplay.Instance.DisplayScore(CurrentLevel);
            DifficultyDisplay.Instance.DisplayQuestionNumber(CurrentLevel);
        }
    }

    public void EndLevel()
    {
        //hide game elements
        //GameSegment.Instance.MoveOut.Play();
        endedLevel = true;
        clearIndexAnswer = true;
        if (Instance.CurrentLevel.CorrectAnswersCount >= 6)
        {
            //totalPoints += CurrentLevel.PointsReceived;
            if (CurrentLevel.Equals(LastLevel))
            {
                endedGame = true;
                nextLevelButton.SetActive(false);
                retryLevelButton.SetActive(true);
                retryNormalButton.SetActive(false);
            }

            else
            {
                nextLevelButton.SetActive(true);
                retryLevelButton.SetActive(false);
                retryNormalButton.SetActive(true);
            }

            ScoreDisplay.Instance.DisplayPartialScore(CurrentLevel);
            ScoreDisplay.Instance.DisplayTotalScore(CurrentLevel);
        }

        else
        {
            nextLevelButton.SetActive(false);
            retryLevelButton.SetActive(true);
            retryNormalButton.SetActive(false);
        }

        //LevelResultDisplay.Instance.Display();
        //endedLevel = false;
        
        Countdown.Instance.Counting = false;
        Debug.Log($"POINTS RECEIVED: {CurrentLevel.PointsReceived}");
        OnLevelEnd.Invoke();
    }

    //invoked by clicking the "Next Level" Button in the performance Interface
    public void NextLevel()
    {
        clearIndexAnswer = true;
        startLevel = true;
        Debug.Log("NextLevel");
        BadgeDisplay.Instance.ResetBigBadge();
        if (CurrentLevel.Equals(LastLevel))
        {
            Debug.Log($"{CurrentLevel} is the last Level. Loading Failed");
            nextLevelButton.SetActive(false);
            retryLevelButton.SetActive(true);
            retryNormalButton.SetActive(false);
            return;
        }
        else if (Instance.CurrentLevel.CorrectAnswersCount < 6)
        {
            ResetLevel();
        }
        else
        {
            //CurrentLevel.TotalPoints += CurrentLevel.PointsReceived;

            endedGame = false;
            BadgeDisplay.Instance.DisplayBadge();
            AnswerButtonsController.Instance.ShowText();
            OnLevelStart.Invoke();

            CurrentLevelIndex++;
            CurrentLevel.CorrectAnswersCount = 0;
            CurrentLevel.PointsReceived = 0;
            CurrentLevel.CurrentChallengeIndex = 0;

            //remove performance display 
            //lalalala

            //show game elements
            DifficultyDisplay.Instance.DisplayQuestionNumber(CurrentLevel);
            ChallengeDisplay.Instance.DisplayChallenge(CurrentChallenge);
        }

        startLevel = false;
    }

    public void ResetLevel()
    {
        startLevel = true;
        OnLevelStart.Invoke();
        AnswerButtonsController.Instance.ShowText();
        endedGame = false;
        CurrentLevel.CorrectAnswersCount = 0;
        CurrentLevel.PointsReceived = 0;
        CurrentLevel.CurrentChallengeIndex = 0;
        ScoreDisplay.Instance.DisplayScore(CurrentLevel);
        DifficultyDisplay.Instance.DisplayQuestionNumber(CurrentLevel);
        ChallengeDisplay.Instance.DisplayChallenge(CurrentChallenge);
        Debug.Log("Repetiu");

    }
}
