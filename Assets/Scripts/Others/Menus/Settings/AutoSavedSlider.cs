using UnityEngine;
using UnityEngine.UI;

internal abstract class AutoSavedSlider : MonoBehaviour {
    [SerializeField] string playerPrefsKey;
    [SerializeField] float defaultValue = 0.5f;

    protected Slider slider => GetComponent<Slider>();

    void Awake() {
        slider.onValueChanged.AddListener(OnValueChange);
        slider.value = PlayerPrefs.GetFloat(playerPrefsKey, defaultValue);
    }

    void Start() {
        InternalValueChange(slider.value);
    }

    public void Reset() {
        OnValueChange(defaultValue);
        slider.value = defaultValue;
    }

    void OnValueChange(float newValue) {
        PlayerPrefs.SetFloat(playerPrefsKey, newValue);
        PlayerPrefs.Save();

        InternalValueChange(newValue);
    }

    protected abstract void InternalValueChange(float newValue);
}
