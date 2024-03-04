using UnityEngine;
using UnityEngine.Audio;

internal class VolumeScript : AutoSavedSlider {
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] string exposedParameterName;

    internal bool mute;

    protected override void InternalValueChange(float newValue) {
        float value = mute ? 0f : newValue;
        audioMixer.SetFloat(exposedParameterName, LinearToDecibel(value));
    }

    float LinearToDecibel(float linear) {
        return (linear != 0) ? 20.0f * Mathf.Log10(linear) : -144.0f;
    }

    internal void Mute(bool isMute) {
        mute = isMute;
        InternalValueChange(slider.value);
    }
}
