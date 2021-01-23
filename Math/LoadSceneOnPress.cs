using UnityEngine;

public class LoadSceneOnPress : MonoBehaviour
{
    [SerializeField] private bool loadWithClick = false;
    [SerializeField] private KeyCode[] loadKeys;
    [SerializeField] private int sceneToLoadIndex;


    private void Update()
    {
        if (loadWithClick && Input.GetMouseButtonDown(0))
        {
            SceneManagerExtension.Instance.LoadScene(sceneToLoadIndex);
            return;
        }

        for (int i = 0; i < loadKeys.Length; i++)
        {
            if (Input.GetKeyDown(loadKeys[i]))
            {
                SceneManagerExtension.Instance.LoadScene(sceneToLoadIndex);
                return;
            }
        }
    }
}
