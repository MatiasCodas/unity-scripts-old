using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public FloatVariable finishedObjectives;
    private GameObject[] objectives;

    public bool winLevel;

    private void Start()
    {
        finishedObjectives = Resources.Load<FloatVariable>("FinishedObjectives");
        objectives = GameObject.FindGameObjectsWithTag("Bola");
    }

    void Update()
    {
        if(finishedObjectives.Value >= objectives.Length)
        {
            winLevel = true;
        }
        else
        {
            winLevel = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Teste");
        }
    }
}
