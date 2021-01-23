using UnityEngine;

[CreateAssetMenu(fileName = "Setting", menuName = "Settings")]
public class Settings : ScriptableObject
{
    [Header("Audio")]
    [Range(0f, 1f)] public float MasterVolume = .5f;
    [Range(0f, 1f)] public float MusicVolume = .5f;
    [Range(0f, 1f)] public float EffectsVolume = .5f;

    [Space(10f)]
    [Header("Visual")]
    public bool IsFullScreen = true;

    [Tooltip("Bigger values mean higher resolutions")]
    public int ResolutionIndex = 21;

    [Tooltip("Bigger values mean higher qualities")]
    [Range(0, 5)]
    public int QualityIndex = 3;

    public void Equate(Settings settings)
    {
        if (settings.Equals(this))
        { return; }


        this.MasterVolume = settings.MasterVolume;
        this.MusicVolume = settings.MusicVolume;
        this.EffectsVolume = settings.EffectsVolume;

        this.IsFullScreen = settings.IsFullScreen;
        this.ResolutionIndex = settings.ResolutionIndex;

        this.QualityIndex = settings.QualityIndex;
    }
}
