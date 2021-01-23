using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    //band-aid solution

    public static DontDestroyOnLoad Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log($"There were multiple Instances of {this} in the scene. Deleting {name}");

            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }
}
