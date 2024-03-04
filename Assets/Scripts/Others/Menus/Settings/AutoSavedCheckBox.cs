using UnityEngine;
using UnityEngine.UI;

internal abstract class AutoSavedCheckBox : MonoBehaviour {
    [SerializeField] string playerPrefsKey;
    [SerializeField] bool defaultValue = false;

    Toggle toggle => GetComponent<Toggle>();

    void Awake() {
        toggle.onValueChanged.AddListener(OnValueChange);
        toggle.isOn = PlayerPrefs.GetInt(playerPrefsKey, defaultValue ? 1 : 0) == 1;
    }

    void Start() {
        InternalValueChange(toggle.isOn);
    }

    public void Reset() {
        OnValueChange(defaultValue);
        toggle.isOn = defaultValue;
    }

    void OnValueChange(bool newValue) {
        PlayerPrefs.SetInt(playerPrefsKey, (newValue ? 1 : 0));
        PlayerPrefs.Save();

        InternalValueChange(newValue);
    }

    protected abstract void InternalValueChange(bool newValue);
}
