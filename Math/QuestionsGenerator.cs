using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QuestionsGenerator : MonoBehaviour
{
    //Variáveis para as perguntas
    public static int num01, num02, num11, num22;
    public static int value01, value02, answer;

    public static int level;
    public static int correctAnswer, totalAnswer;
    public static int wrongAnswer01, wrongAnswer02, wrongAnswer03;
    public int repeatedAnswer;

    public static bool done;

    public Text count01, answerButton;
    public Text wrongButton01, wrongButton02, wrongButton03;
    public GameObject[] buttons;

    public int randomPos;
    public int[] lastPos;
    public Transform[] places;

    private void Start()
    {
        level = 1;
    }

    private void Update()
    {
        if (!done)
        {
            if (level.Equals(1))
            {
                //testing
                //Level01();
                SetChallenge(1, 6);


            }

            else if (level.Equals(2))
            {
                Level02();

                if (repeatedAnswer.Equals(answer))
                {
                    Level02();
                }
            }

            else if (level.Equals(3))
            {
                Level03();

                if (repeatedAnswer.Equals(answer))
                {
                    Level03();
                }
            }

            else if (level.Equals(4))
            {
                Level04();

                if (repeatedAnswer.Equals(answer))
                {
                    Level04();
                }
            }

            else if (level.Equals(5))
            {
                Level05();

                if (repeatedAnswer.Equals(answer))
                {
                    Level05();
                }
            }
        }

        //Para trocar de level

        if (totalAnswer.Equals(10))
        {
            level++;
            totalAnswer = 0;
            UnityEngine.SceneManagement.SceneManager.LoadScene("SOCCER_Performance");
        }

    }

    #region Old Design

    private void Level01()
    {
        //Gerando a conta
        num01 = Random.Range(1, 6);
        num02 = Random.Range(1, 6);

        answer = num01 + num02;

        //Gerando as respostas erradas
        wrongAnswer01 = answer + Random.Range(-3, 3);
        wrongAnswer02 = answer + Random.Range(-3, 3);
        wrongAnswer03 = answer + Random.Range(-3, 3);

        while (wrongAnswer01 <= 0 || wrongAnswer01 > 10 || wrongAnswer01.Equals(answer))
        {
            wrongAnswer01 = answer + Random.Range(-3, 3);
        }

        while (wrongAnswer02.Equals(wrongAnswer01) || wrongAnswer02 <= 0 || wrongAnswer02 > 10 || wrongAnswer02.Equals(answer))
        {
            wrongAnswer02 = answer + Random.Range(-3, 3);
        }

        while (wrongAnswer03.Equals(wrongAnswer02) || wrongAnswer03.Equals(wrongAnswer01) || wrongAnswer03 <= 0 || wrongAnswer03 > 10 || wrongAnswer03.Equals(answer))
        {
            wrongAnswer03 = answer + Random.Range(-3, 3);
        }


        repeatedAnswer = answer;

        done = true;

        Debug.Log(answer);



        RandomPlaceButton();

    }

    private void Level02()
    {
        //Gerando a conta
        num01 = Random.Range(1, 11);
        num02 = Random.Range(1, 11);

        answer = num01 + num02;

        //Gerando as respostas erradas

        wrongAnswer01 = answer + Random.Range(-3, 3);
        wrongAnswer02 = answer + Random.Range(-3, 3);
        wrongAnswer03 = answer + Random.Range(-3, 3);

        while (wrongAnswer01 <= 0 || wrongAnswer01.Equals(answer))
        {
            wrongAnswer01 = answer + Random.Range(-3, 3);
        }

        while (wrongAnswer02.Equals(wrongAnswer01) || wrongAnswer02 <= 0 || wrongAnswer02.Equals(answer))
        {
            wrongAnswer02 = answer + Random.Range(-3, 3);
        }

        while (wrongAnswer03.Equals(wrongAnswer02) || wrongAnswer03.Equals(wrongAnswer01) || wrongAnswer03 <= 0 || wrongAnswer03.Equals(answer))
        {
            wrongAnswer03 = answer + Random.Range(-3, 3);
        }


        //Colocando no canvas os resultados
        count01.text = num01 + "+" + num02 + "?";
        answerButton.text = answer + "";
        wrongButton01.text = wrongAnswer01 + "";
        wrongButton02.text = wrongAnswer02 + "";
        wrongButton03.text = wrongAnswer03 + "";

        done = true;

        repeatedAnswer = answer;

        RandomPlaceButton();
    }

    private void Level03()
    {

        //Gerando a conta
        num01 = Random.Range(1, 5);
        num02 = Random.Range(1, 5);
        num11 = Random.Range(0, 5);
        num22 = Random.Range(0, 5);

        value01 = (num01 * 10) + num11;
        value02 = (num02 * 10) + num22;

        answer = value01 + value02;

        //Gerando as respostas erradas

        wrongAnswer01 = answer + Random.Range(-6, 6);
        wrongAnswer02 = answer + Random.Range(-6, 6);
        wrongAnswer03 = answer + Random.Range(-6, 6);

        while (wrongAnswer01 <= 0 || wrongAnswer01.Equals(answer))
        {
            wrongAnswer01 = answer + Random.Range(-6, 6);
        }

        while (wrongAnswer02.Equals(wrongAnswer01) || wrongAnswer02 <= 0 || wrongAnswer02.Equals(answer))
        {
            wrongAnswer02 = answer + Random.Range(-6, 6);
        }

        while (wrongAnswer03.Equals(wrongAnswer02) || wrongAnswer03.Equals(wrongAnswer01) || wrongAnswer03 <= 0 || wrongAnswer03.Equals(answer))
        {
            wrongAnswer03 = answer + Random.Range(-6, 6);
        }

        //Colocando no canvas os resultados

        count01.text = value01 + "+" + value02 + "?";
        answerButton.text = answer + "";
        wrongButton01.text = wrongAnswer01 + "";
        wrongButton02.text = wrongAnswer02 + "";
        wrongButton03.text = wrongAnswer03 + "";

        done = true;

        repeatedAnswer = answer;

        RandomPlaceButton();
    }

    private void Level04()
    {

        //Gerando a conta
        num01 = Random.Range(1, 11);
        num02 = Random.Range(1, 11);

        //Colocando na ordem certa a conta de subtração
        if (num01 > num02)
        {
            answer = num01 - num02;
            count01.text = num01 + "-" + num02 + "?";
        }

        else
        {
            answer = num02 - num01;
            count01.text = num02 + "-" + num01 + "?";
        }

        //Gerando as respostas erradas

        wrongAnswer01 = answer + Random.Range(-3, 8);
        wrongAnswer02 = answer + Random.Range(-3, 8);
        wrongAnswer03 = answer + Random.Range(-3, 8);

        while (wrongAnswer01 <= 0 || wrongAnswer01.Equals(answer))
        {
            wrongAnswer01 = answer + Random.Range(-3, 8);
        }

        while (wrongAnswer02.Equals(wrongAnswer01) || wrongAnswer02 <= 0 || wrongAnswer02.Equals(answer))
        {
            wrongAnswer02 = answer + Random.Range(-3, 8);
        }

        while (wrongAnswer03.Equals(wrongAnswer02) || wrongAnswer03.Equals(wrongAnswer01) || wrongAnswer03 <= 0 || wrongAnswer03.Equals(answer))
        {
            wrongAnswer03 = answer + Random.Range(-3, 8);
        }


        //Colocando no canvas os resultados

        answerButton.text = answer + "";
        wrongButton01.text = wrongAnswer01 + "";
        wrongButton02.text = wrongAnswer02 + "";
        wrongButton03.text = wrongAnswer03 + "";

        done = true;

        repeatedAnswer = answer;

        RandomPlaceButton();
    }

    private void Level05()
    {

        //Gerando a conta
        num01 = Random.Range(1, 5);
        num02 = Random.Range(1, 5);
        num11 = Random.Range(0, 5);
        num22 = Random.Range(0, 5);

        value01 = (num01 * 10) + num11;
        value02 = (num02 * 10) + num22;

        //Colocando na ordem certa a conta de subtração

        if (value01 > value02)
        {
            answer = value01 - value02;
            count01.text = value01 + "-" + value02 + "?";
        }

        else
        {
            answer = value02 - value01;
            count01.text = value02 + "-" + value01 + "?";
        }


        //Gerando as respostas erradas

        wrongAnswer01 = answer + Random.Range(-8, 8);
        wrongAnswer02 = answer + Random.Range(-8, 8);
        wrongAnswer03 = answer + Random.Range(-8, 8);

        while (wrongAnswer01 <= 0 || wrongAnswer01.Equals(answer))
        {
            wrongAnswer01 = answer + Random.Range(-8, 8);
        }

        while (wrongAnswer02.Equals(wrongAnswer01) || wrongAnswer02 <= 0 || wrongAnswer02.Equals(answer))
        {
            wrongAnswer02 = answer + Random.Range(-8, 8);
        }

        while (wrongAnswer03.Equals(wrongAnswer02) || wrongAnswer03.Equals(wrongAnswer01) || wrongAnswer03 <= 0 || wrongAnswer03.Equals(answer))
        {
            wrongAnswer03 = answer + Random.Range(-8, 8);
        }

        //Colocando no canvas os resultados

        answerButton.text = answer + "";
        wrongButton01.text = wrongAnswer01 + "";
        wrongButton02.text = wrongAnswer02 + "";
        wrongButton03.text = wrongAnswer03 + "";

        done = true;

        repeatedAnswer = answer;

        RandomPlaceButton();
    }

    private void RandomPlaceButton()
    {
        //Colocando os botões em lugares aleatórios

        randomPos = Random.Range(0, places.Length);

        buttons[0].transform.position = places[randomPos].transform.position;

        if (randomPos.Equals(0))
        {
            buttons[1].transform.position = places[1].transform.position;
            buttons[2].transform.position = places[2].transform.position;
            buttons[3].transform.position = places[3].transform.position;
        }

        else if (randomPos.Equals(1))
        {
            buttons[1].transform.position = places[0].transform.position;
            buttons[2].transform.position = places[2].transform.position;
            buttons[3].transform.position = places[3].transform.position;
        }

        else if (randomPos.Equals(2))
        {
            buttons[1].transform.position = places[1].transform.position;
            buttons[2].transform.position = places[0].transform.position;
            buttons[3].transform.position = places[3].transform.position;
        }

        else if (randomPos.Equals(3))
        {
            buttons[1].transform.position = places[0].transform.position;
            buttons[2].transform.position = places[1].transform.position;
            buttons[3].transform.position = places[2].transform.position;
        }
    }

    #endregion

    #region NewDesign

    private void SetChallenge(int minVariable, int maxVariable)
    {
        int result = GetResult(minVariable, maxVariable);
        int[] wrongAnswers = GetWrongAnswers(result, 3);

        //testing
        answer = result;
        wrongAnswer01 = wrongAnswers[0];
        wrongAnswer02 = wrongAnswers[1];
        wrongAnswer03 = wrongAnswers[2];

        repeatedAnswer = result; //why?
        done = true; //why?
        Debug.Log($"Correct Answer: {result}");


        PlaceButtons();
    }

    private int GetResult(int min, int max)
    {
        //Gerando a conta
        num01 = Random.Range(min, max);
        num02 = Random.Range(min, max);

        return num01 + num02;
    }

    private int[] GetWrongAnswers(int correctAnswer, int variance)
    {
        //Gerando as respostas erradas
        int[] wrongAnswers = new int[3];

        for (int i = 0; i < wrongAnswers.Length; i++)
        {
            //cria uma resposta errada somando um valor entre variância negativa e positiva
            wrongAnswers[i] = correctAnswer;

            //compara o valor gerado com os valores que já foram gerados. Se for igual, recalcula
            //todo: fazer design cuja duração não dependa de um random "sortudo"
            //keep track of how many times it looped for testing purposes
            //todo: remove loopCounting
            int loopCount = 0;
            while (wrongAnswers[i].Equals(correctAnswer) ||
                wrongAnswers[i] < 1 ||
                IsEqualToPreviousValuesInArray(wrongAnswers[i], wrongAnswers, i))
            {
                loopCount++;
                wrongAnswers[i] = correctAnswer + Random.Range(-variance, variance);
            }
            Debug.Log($"Wrong Answer of index {i} and value {wrongAnswers[i]} was generated {loopCount} times");
        }

        return wrongAnswers;
    }

    private bool IsEqualToPreviousValuesInArray(int value, int[] comparedArray, int comparedIndex)
    {
        bool r = false;

        for (int i = 0; i < comparedIndex; i++)
        {
            //loops through the elements with and index smaller than the compared index
            //returns true if it finds a duplicate
            r = value.Equals(comparedArray[i]);
            if (r)
            {
                return r;
            }
        }

        return r;
    }

    private void PlaceButtons()
    {
        //get list from possible positions
        List<Transform> positions = new List<Transform>(places);

        //loop through the button array
        for (int i = 0; i < buttons.Length; i++)
        {
            //get a random position from the available positions in the list
            int posIndex = Random.Range(0, positions.Count);
            //place the button accordingly
            buttons[i].transform.position = positions[posIndex].position;
            //remove the "used" position 
            positions.RemoveAt(posIndex);
        }
    }

    #endregion

}
