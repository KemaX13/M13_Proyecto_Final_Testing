using UnityEngine;
using UnityEngine.Events;

internal class HealthBehaviour : MonoBehaviour {
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;

    [SerializeField] bool Inmortal;

    [SerializeField] UnityEvent OnDie;
    [SerializeField] UnityEvent<float> OnChangeHealth;

    void OnEnable() {
        currentHealth = maxHealth;
        Heal(currentHealth);
    }

    public void Heal(float health) {
        currentHealth = Mathf.Min(currentHealth + health, maxHealth);
        OnChangeHealth.Invoke(currentHealth / maxHealth);
    }

    public void Hurt(float damage) {
        if(Inmortal)
            return;
        
        currentHealth = Mathf.Max(currentHealth - damage, 0);

        if(currentHealth == 0)
            OnDie.Invoke();

        OnChangeHealth.Invoke(currentHealth / maxHealth);
    }

    public void OnInmortal() {
        Inmortal = !Inmortal;
    }
}
