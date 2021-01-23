using System.Collections.Generic;
using UnityEngine;

public class SoccerLevel
{
    //public ChallengeType Type = ChallengeType.Unit;

    [SerializeField] public int challengeCount = 10;
    public int PointsReceived;
    public List<SoccerChallenge> Challenges { get; private set; }
    public SoccerChallenge CurrentChallenge => Challenges[CurrentChallengeIndex];
    public int CorrectAnswersCount = 0;
    public int CurrentChallengeIndex = 0;


    public SoccerChallenge FirstChallenge => Challenges[0];
    public SoccerChallenge LastChallenge => Challenges[Challenges.Count - 1];

    public SoccerLevel(ChallengeType[] possibleTypes, int challengeCount)
    {
        //correct constructor
        this.challengeCount = challengeCount;
        Challenges = new List<SoccerChallenge>();
        for (int i = 0; i < challengeCount; i++)
        {
            if (possibleTypes.Length > 1)
            {
                if (i < 5)
                {
                    Challenges.Add(new SoccerChallenge(possibleTypes[0]));
                }

                else
                {
                    Challenges.Add(new SoccerChallenge(possibleTypes[1]));
                }
            }

            else
            {
                Challenges.Add(new SoccerChallenge(possibleTypes[0]));
            }

            //ChallengeType randomType = possibleTypes[Random.Range(0, possibleTypes.Length - 1)]; //-1 since max value of random.range is inclusive
            //Challenges.Add(new SoccerChallenge(randomType));
        }
    }

    public SoccerLevel()
    {
        //for testing purposes
        challengeCount = 1;
        Challenges = new List<SoccerChallenge>();
        Challenges.Add(new SoccerChallenge(ChallengeType.Unit));
    }
}
