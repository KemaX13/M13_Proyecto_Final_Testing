using UnityEngine;

internal class FullSceenScript : AutoSavedCheckBox {
    protected override void InternalValueChange(bool newValue) => Screen.fullScreen = newValue;
}
