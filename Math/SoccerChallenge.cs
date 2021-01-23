using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerChallenge
{
    public ChallengeType ChallengeType;
    public int[] Variables;

    public int PointsReceived = 0;
    public static int scorePoints;
    public static bool blocked;
    public static bool floped;
    public static bool fireBall;


    [Tooltip("Points receved when answering correctly")]
    [SerializeField] private int defaultPoints = 10;
    [Tooltip("Points receved when answering correctly AND in a short time")]
    [SerializeField] private int quickAnswerPoints = 20;

    [Tooltip("Time in seconds the player has to answer quickly")]
    [SerializeField] private float quickAnswerTime = 4f;
    private float elapsed = 0f;
    private bool quicklyAnswered = false;

    public SoccerAnswer[] PossibleAnswers;
    public SoccerAnswer CorrectAnswer;
    public SoccerAnswer[] WrongAnswers;

    public SoccerAnswer SelectedAnswer;

    public bool IsSubtraction
    {
        get
        {
            for (int i = 0; i < Variables.Length; i++)
            {
                if (CorrectAnswer.Value < Variables[i])
                {
                    return true;
                }
            }
            return false;
        }
    }

    public bool AnsweredCorrectly { get; private set; }
    private List<int> validAnswers;

    private EventInstance feedbackEvent;
    private PARAMETER_ID feedbackParameter;


    public SoccerChallenge(ChallengeType type)
    {
        this.ChallengeType = type;
        //todo: transformar num Dictionary ou HashTable
        if (type.Equals(ChallengeType.Golden))
        {
            Initialize(type, 2, 1, 6, 4, 9, false, false, true);
        }
        else if (type.Equals(ChallengeType.Unit))
        {
            Initialize(type, 2, 1, 6, 4, 9, false, false, true);
        }
        else if (type.Equals(ChallengeType.Ten))
        {
            Initialize(type, 2, 5, 49, 9, 98, false, false, false);
        }
        else if (type.Equals(ChallengeType.TenSpare))
        {
            Initialize(type, 2, 5, 49, 9, 98, true, false, false);
        }
        else if (type.Equals(ChallengeType.UnitSub))
        {
            Initialize(type, 2, 1, 9, 4, 8, false, true, true);
        }
        else if (type.Equals(ChallengeType.TenSub))
        {
            Initialize(type, 2, 11, 99, 49, 88, false, true, false);
        }
        else if (type.Equals(ChallengeType.TenSubSpare))
        {
            Initialize(type, 2, 11, 99, 49, 88, true, true, false);
        }
        else if (type.Equals(ChallengeType.Hundred))
        {
            Initialize(type, 2, 10, 499, 99, 100, true, false, false);
        }

    }

    public SoccerChallenge(ChallengeType type, int variableCount, int minVariable, int maxVariable, int wrongAnswerVariant, int maxWrong, bool countWithSpare, bool isSubtraction, bool isUnit)
    {
        Initialize(type, variableCount, minVariable, maxVariable, wrongAnswerVariant, maxWrong, countWithSpare, isSubtraction, isUnit);
    }

    public void Initialize(ChallengeType type, int variableCount, int minVariable, int maxVariable, int wrongAnswerVariant, int maxWrong, bool countWithSpare, bool isSubtraction, bool isUnit)
    {

        //todo: make the ammount of possible answers bigger and flexible. Right now its hardcoded
        this.ChallengeType = type;

        //initialize PossibleAnswers
        PossibleAnswers = new SoccerAnswer[4];

        for (int i = 0; i < PossibleAnswers.Length; i++)
        {

            PossibleAnswers[i] = new SoccerAnswer();

        }

        //set correct and wrong answers
        CorrectAnswer = PossibleAnswers[0];





        Variables = new int[variableCount];

        for (int i = 0; i < Variables.Length; i++)
        {

            Variables[i] = Random.Range(minVariable, maxVariable);
            if (!isSubtraction)
            {
                if (!countWithSpare)
                {
                    while ((Variables[0] % 10) + (Variables[1] % 10) >= 10)
                    {
                        Variables[i] = Random.Range(minVariable, maxVariable);
                    }
                }
                else
                {
                    if ((Variables[0] % 10) + (Variables[1] % 10) < 10)
                    {

                        Variables[0] -= Variables[0] % 10;
                        Variables[0] += Random.Range(6, 9);

                        Variables[1] -= Variables[1] % 10;
                        Variables[1] += Random.Range(6, 9);

                    }
                }
                CorrectAnswer.Value = Variables[0] + Variables[1];
            }
            else
            {
                if (Variables[0] >= Variables[1])
                {
                    if (!countWithSpare)
                    {
                        while ((Variables[0] % 10) < (Variables[1] % 10))
                        {
                            Variables[i] = Random.Range(minVariable, maxVariable);
                        }
                    }
                    else
                    {
                        if ((Variables[0] % 10) >= (Variables[1] % 10))
                        {
                            Variables[0] -= Variables[0] % 10;
                            Variables[0] += Random.Range(1, 5);

                            Variables[1] -= Variables[1] % 10;
                            Variables[1] += Random.Range(6, 9);
                        }

                        if ((Variables[0] - (Variables[0] % 10)) == (Variables[1] - Variables[1] % 10))
                        {
                            if ((Variables[0] - (Variables[0] % 10)) == 90)
                            {
                                Variables[1] -= 10;
                            }

                            else if ((Variables[0] - (Variables[0] % 10)) == 10)
                            {
                                Variables[0] += 10;
                            }

                            else
                            {
                                Variables[0] += 10;
                            }
                        }
                    }
                    CorrectAnswer.Value = Variables[0] - Variables[1];
                }
                else
                {
                    if (!countWithSpare)
                    {
                        while ((Variables[1] % 10) < (Variables[0] % 10))
                        {
                            Variables[i] = Random.Range(minVariable, maxVariable);
                        }
                    }
                    else
                    {

                        if ((Variables[1] % 10) >= (Variables[0] % 10))
                        {
                            Variables[1] -= Variables[1] % 10;
                            Variables[1] += Random.Range(1, 5);

                            Variables[0] -= Variables[1] % 10;
                            Variables[0] += Random.Range(6, 9);
                        }

                        if ((Variables[0] - (Variables[0] % 10)) == (Variables[1] - Variables[1] % 10))
                        {
                            if ((Variables[1] - (Variables[0] % 10)) == 90)
                            {
                                Variables[0] -= 10;
                            }
                            else if ((Variables[0] - (Variables[0] % 10)) == 10)
                            {
                                Variables[1] += 10;
                            }

                            else
                            {
                                Variables[1] += 10;
                            }
                        }
                    }
                    CorrectAnswer.Value = Variables[1] - Variables[0];
                }
            }
        }

        WrongAnswers = GetWrongAnswers(PossibleAnswers.Length - 1, CorrectAnswer, minVariable * variableCount, maxWrong, countWithSpare, isUnit); //variance is hardcoded. I think it should depend on ChallegeType


        feedbackEvent = RuntimeManager.CreateInstance(StaticFmodRefs.SoccerRefs.PATH_FEEDBACK);
        feedbackParameter = StaticFmodRefs.GetID(feedbackEvent, StaticFmodRefs.SoccerRefs.PARAMETER_FEEDBACK);

        //testing
        //Debug.Log($"Correct Answer: {CorrectAnswer.Value}");


    }



    private SoccerAnswer[] GetWrongAnswers(int amount, SoccerAnswer correctAnswer, int minResult, int maxWrong, bool countWithSpare, bool isUnit)
    {

        SoccerAnswer[] newWrongAnswers = new SoccerAnswer[amount];

        //cria uma lista com todos os valores válidos
        validAnswers = new List<int>();

        for (int j = 0; j < maxWrong /*- minResult*/; j++)
        {
            validAnswers.Add(minResult + j);

        }
        validAnswers.Remove(correctAnswer.Value);

        for (int i = 0; i < amount; i++)
        {
            //referencia uma resposta errada
            newWrongAnswers[i] = PossibleAnswers[i + 1];
            //Debug.Log($"{newWrongAnswers[i].Value} e {PossibleAnswers[i + 1].Value}");


            //remove a resposta correta da lista de possíveis valores

            int randomIndex = Random.Range(0, validAnswers.Count - 1); //escolhe um valor aleatório dentre os possíveis

            //Debug.Log(validAnswers[randomIndex]);

            newWrongAnswers[i].Value = validAnswers[randomIndex]; //coloca esse valor dentro da resposta errada sendo gerada


            validAnswers.Remove(newWrongAnswers[i].Value); //remove a repsosta escolhida da lista de possíveis respostas

            if (!isUnit)
            {
                if (correctAnswer.Value < 10)
                {
                    newWrongAnswers[0].Value = correctAnswer.Value + 10;
                }
                else
                {

                    newWrongAnswers[0].Value = (correctAnswer.Value + (Random.Range(-1, 1) * 10));

                    while (newWrongAnswers[0].Value == correctAnswer.Value)
                    {
                        newWrongAnswers[0].Value += (Random.Range(-1, 1) * 10);
                    }
                }
            }

        }




        return newWrongAnswers;
    }


    //var validChoices = [1, 3, 5];
    //function GetRandom() : int
    //{
    //     return validChoices[Random.Range(0, validChoices.Length)];
    //}

    private bool IsEqualToPreviousValuesInArray(SoccerAnswer comparingAnswer, SoccerAnswer[] comparedAnswers, int comparedIndex)
    {
        //loops through the elements with and index smaller than the compared index
        for (int i = 0; i < comparedIndex; i++)
        {
            //if it finds a duplicate, retur true
            if (comparingAnswer.Value.Equals(comparedAnswers[i].Value))
            {
                return true;
            }
        }

        //if no duplicate was found, return false
        return false;
    }



    public IEnumerator QuickAnswerTimer()
    {
        quicklyAnswered = true;

        //todo: maybe change to a WaitForSeconds?
        elapsed = 0f;
        while (elapsed < quickAnswerTime)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        quicklyAnswered = false;
        //fireBall = false;
    }



    public void SelectAnswer(SoccerAnswer selectedAnswer)
    {
        SelectedAnswer = selectedAnswer;
        AnsweredCorrectly = SelectedAnswer.Equals(CorrectAnswer);

        if (AnsweredCorrectly)
        {
            if (quicklyAnswered)
            {
                fireBall = true;
            }

            //Debug.Log($"{this} was answered CORRECTLY. Selected Answer: {selectedAnswer.Value}  ---  Correct Answer: {CorrectAnswer.Value}");
            PointsReceived += quicklyAnswered ? quickAnswerPoints : defaultPoints;

            //scorePoints += quicklyAnswered ? quickAnswerPoints : defaultPoints;
            SoccerManager.Instance.CurrentLevel.CorrectAnswersCount++;
            floped = true;

            feedbackEvent.setParameterByID(feedbackParameter, 1f);

        }
        else
        {

            //Debug.Log($"{this} was answered INCORRECTLY. Selected Answer: {selectedAnswer.Value}  ---  Correct Answer: {CorrectAnswer.Value}");
            PointsReceived = 0;
            blocked = true;
            fireBall = false;

            feedbackEvent.setParameterByID(feedbackParameter, 0f);
        }

        feedbackEvent.start();

        //SoccerManager.Instance.NextChallenge();
    }
}

public enum ChallengeType
{
    Golden,
    Unit,
    Ten,
    TenSpare,
    UnitSub,
    TenSub,
    TenSubSpare,
    Hundred
}
