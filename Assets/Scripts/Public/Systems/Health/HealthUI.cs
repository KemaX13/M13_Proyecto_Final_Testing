using UnityEngine;
using UnityEngine.UI;

internal class HealthUI : MonoBehaviour {
    [SerializeField] Slider healthSlider;

    public void UpdateHealthBar(float currentValue) {
        healthSlider.value = currentValue;
    }
}
