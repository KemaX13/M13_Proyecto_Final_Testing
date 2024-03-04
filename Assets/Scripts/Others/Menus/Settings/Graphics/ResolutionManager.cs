using System.Collections.Generic;
using UnityEngine;
using TMPro;

internal class ResolutionManager : MonoBehaviour {
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] string playerPrefsKey;

    [Space(10), SerializeField] Vector2 aspectRatio;
    [SerializeField] bool swap;

    Resolution[] resolutions;
    int savedIndex;
    int k;

    void Start() {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        k = 0;

        for(int i = 0; i < resolutions.Length; i++) {
            float aspectRatioCheck = (float)resolutions[i].width / resolutions[i].height;
            float aspectRatioDesired = aspectRatio.x / aspectRatio.y;

            if(Mathf.Approximately(aspectRatioCheck, aspectRatioDesired)) {
                int swappedWidth = swap ? resolutions[i].height : resolutions[i].width;
                int swappedHeight = swap ? resolutions[i].width : resolutions[i].height;

                string option = swappedWidth + " x " + swappedHeight;
                options.Add(option);

                k++;

                if(swappedWidth == Screen.width && swappedHeight == Screen.height) {
                    savedIndex = k;

                    break;
                }
            }
        }

        resolutionDropdown.AddOptions(options);

        int index = PlayerPrefs.GetInt(playerPrefsKey, savedIndex);
        resolutionDropdown.value = index;
        resolutionDropdown.RefreshShownValue();

        SetResolution(index);
    }

    public void SetResolution(int index) {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, false);

        PlayerPrefs.SetInt(playerPrefsKey, index);
        PlayerPrefs.Save();

        Debug.Log(Screen.currentResolution);
    }

    public void ResetResolution() {
        SetResolution(savedIndex);

        resolutionDropdown.value = savedIndex;
        resolutionDropdown.RefreshShownValue();
    }
}