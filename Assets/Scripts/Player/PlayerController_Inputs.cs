using UnityEngine;
using UnityEngine.InputSystem;

internal partial class PlayerController : EntityController {
    #region SerializeField Variables
    [Header("Inputs")]
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference jump;
    [SerializeField] InputActionReference dash;
    #endregion

    void OnEnable() {
        move.action.Enable();
        jump.action.Enable();
        dash.action.Enable();
    }

    void OnDisable() {
        move.action.Disable();
        jump.action.Disable();
        dash.action.Disable();
    }

    void StartInput() {
        jump.action.started += OnJump;
        jump.action.canceled += OnJump;

        dash.action.started += OnSprint;
        dash.action.canceled += OnSprint;
    }
}
