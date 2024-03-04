using UnityEngine;

internal class QualityManager : MonoBehaviour {
    [SerializeField] string playerPrefsKey;
    [SerializeField] int defaultValue = 1;

    void Start() {
        int index = PlayerPrefs.GetInt(playerPrefsKey, defaultValue);

        SetQuality(index);
    }

    public void ResetQuality() => SetQuality(defaultValue);

    public void SetQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);

        PlayerPrefs.SetInt(playerPrefsKey, qualityIndex);
        PlayerPrefs.Save();
    }
}
