using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenMaterial : MonoBehaviour
{

    public GameObject[] goldenMaterialCorrect;

    public GameObject[] goldenMaterialWrong01;

    public GameObject[] goldenMaterialWrong02;

    public GameObject[] goldenMaterialWrong03;

    public GameObject[] goldenMaterialQuestion01;

    public GameObject[] goldenMaterialQuestion02;

    private int questionIndex01;

    private int questionIndex02;

    private int correctIndex;

    private int wrongIndex01;

    private int wrongIndex02;

    private int wrongIndex03;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(QuestionsGenerator.level == 1)
        {
            questionIndex01 = QuestionsGenerator.num01;
            questionIndex02 = QuestionsGenerator.num02;
            correctIndex = QuestionsGenerator.answer;
            wrongIndex01 = QuestionsGenerator.wrongAnswer01;
            wrongIndex02 = QuestionsGenerator.wrongAnswer02;
            wrongIndex03 = QuestionsGenerator.wrongAnswer03;


            //Resetando o material dourado
            for(int i = 0; i < 5; i++)
            {
                goldenMaterialQuestion01[i].SetActive(false);
                goldenMaterialQuestion02[i].SetActive(false);
            }

            for (int i = 0; i < 10; i++)
            {
                goldenMaterialCorrect[i].SetActive(false);
                goldenMaterialWrong01[i].SetActive(false);
                goldenMaterialWrong02[i].SetActive(false);
                goldenMaterialWrong03[i].SetActive(false);
            }

            //Tornando ativo o material dourado
            for (int i = 0; i < questionIndex01; i++)
            {
                if (QuestionsGenerator.done)
                {
                    goldenMaterialQuestion01[i].SetActive(true);
                }
                            }

            for (int i = 0; i < questionIndex02; i++)
            {
                if (QuestionsGenerator.done)
                {
                    goldenMaterialQuestion02[i].SetActive(true);
                }

            }

            for (int i = 0; i < correctIndex; i++)
            {
                if (QuestionsGenerator.done)
                {
                    goldenMaterialCorrect[i].SetActive(true);
                }
                else
                {
                    goldenMaterialCorrect[i].SetActive(false);
                }
            }

            for (int i = 0; i < wrongIndex01; i++)
            {
                if (QuestionsGenerator.done)
                {
                    goldenMaterialWrong01[i].SetActive(true);
                }
                else
                {
                    goldenMaterialWrong01[i].SetActive(false);
                }
            }

            for (int i = 0; i < wrongIndex02; i++)
            {
                if (QuestionsGenerator.done)
                {
                    goldenMaterialWrong02[i].SetActive(true);
                }
                else
                {
                    goldenMaterialWrong02[i].SetActive(false);
                }
            }

            for (int i = 0; i < wrongIndex03; i++)
            {
                if (QuestionsGenerator.done)
                {
                    goldenMaterialWrong03[i].SetActive(true);
                }
                else
                {
                    goldenMaterialWrong03[i].SetActive(false);
                }
            }
        }

        else
        {

            for (int i = 0; i < 5; i++)
            {
                goldenMaterialQuestion01[i].SetActive(false);
                goldenMaterialQuestion02[i].SetActive(false);
            }

            for (int i = 0; i < 10; i++)
            {
                goldenMaterialCorrect[i].SetActive(false);
                goldenMaterialWrong01[i].SetActive(false);
                goldenMaterialWrong02[i].SetActive(false);
                goldenMaterialWrong03[i].SetActive(false);
            }

        }



    
        
    }
}
