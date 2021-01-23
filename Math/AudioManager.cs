using FMOD.Studio;
using FMODUnity;
using UnityEngine;
public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private Settings storedSettings;

    public Bus masterBus;
    public Bus musicBus;
    public Bus effectsBus;

    public static AudioManager Instance;
    private void Awake()
    {
        #region Singleton

        if (Instance != null)
        {
            Debug.Log($"AudioManager instance already exists. Deleting {this}");
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
        #endregion

        //DontDestroyOnLoad(this);

        masterBus = RuntimeManager.GetBus("bus:/Master");
        musicBus = RuntimeManager.GetBus("bus:/Master/Music");
        effectsBus = RuntimeManager.GetBus("bus:/Master/Effects");

        masterBus.setVolume(storedSettings.MasterVolume);
        musicBus.setVolume(storedSettings.MusicVolume);
        effectsBus.setVolume(storedSettings.EffectsVolume);
    }


}
