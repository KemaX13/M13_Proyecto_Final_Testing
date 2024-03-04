using UnityEngine;

internal class BrightnessScript : AutoSavedSlider {
    protected override void InternalValueChange(float newValue) {
        Screen.brightness = newValue;
    }
}
