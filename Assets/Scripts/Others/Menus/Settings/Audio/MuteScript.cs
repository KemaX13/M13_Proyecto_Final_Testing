using UnityEngine;
using UnityEngine.UI;

internal class MuteScript : AutoSavedCheckBox {
    [Space(10), SerializeField] Slider slider;

    VolumeScript volume => slider.GetComponent<VolumeScript>();

    protected override void InternalValueChange(bool newValue) {
        volume.Mute(newValue);
    }
}
