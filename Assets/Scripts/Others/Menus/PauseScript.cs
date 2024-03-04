using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

internal class PauseScript : MonoBehaviour {
    [SerializeField] InputActionReference pause;
    [SerializeField] Canvas pauseCanvas;

    [SerializeField] UnityEvent onPauseEvent;
    [SerializeField] UnityEvent onResumeEvent;

    void OnEnable() {
        pause.action.Enable();
        pause.action.performed += OnPause;
    }

    void OnDisable() {
        pause.action.Disable();
        pause.action.performed -= OnPause;
    }

    void OnPause(InputAction.CallbackContext input) {
        pauseCanvas.enabled = !pauseCanvas.enabled;
        
        if(pauseCanvas.enabled)
            onPauseEvent.Invoke();
        else
            onResumeEvent.Invoke();
    }

    public void OnResume() {
        pauseCanvas.enabled = false;
        onResumeEvent.Invoke();
    }
}
