using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerExtension : MonoBehaviour
{
    [SerializeField] private float fadeTime = 1f;

    private Image fadeImage;


    public Action OnSceneLoad;
    public Action OnSceneUnload;


    public static SceneManagerExtension Instance;
    private void Awake()
    {
        #region Singleton

        if (Instance != null)
        {
            Debug.Log($"There are multiple SceneManagerExtensions in the scene. Deleting {name}");
            Destroy(this);
            return;
        }

        Instance = this;

        #endregion

        fadeImage = GetComponent<Image>();
        fadeImage.color = new Color(0f, 0f, 0f, 0f);
    }

    public void LoadScene(int sceneIndex)
    {
        Debug.Log($"Loading Scene {sceneIndex}");

        OnSceneUnload?.Invoke();
        StartCoroutine(FadeScene(sceneIndex, fadeTime));
    }

    private IEnumerator FadeScene(int sceneIndex, float fadeTime)
    {
        Color fadeColor = fadeImage.color;

        for (float elapsed = 0; elapsed < fadeTime; elapsed += Time.deltaTime)
        {
            fadeColor.a = elapsed / fadeTime;
            fadeImage.color = fadeColor;

            yield return null;
        }
        fadeImage.color = Color.black;

        SceneManager.LoadScene(sceneIndex);
        OnSceneLoad?.Invoke();
    }

}
