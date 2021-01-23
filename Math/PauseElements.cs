using UnityEngine;

public class PauseElements : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsPanel;

    public void SetPauseState(bool pauseState)
    {
        GameManager.Instance.SetPauseState(pauseState);
    }

    public void ChangePauseState()
    {
        SetPauseState(!GameManager.Instance.GamePaused);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
    }
}