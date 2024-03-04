using UnityEngine;

internal class DontDestroyOnSceneLoad : MonoBehaviour
{
    static DontDestroyOnSceneLoad instance;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
            Destroy(this);
    }
}
