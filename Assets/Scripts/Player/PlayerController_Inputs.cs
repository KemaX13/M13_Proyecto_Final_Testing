using UnityEngine;
using UnityEngine.InputSystem;

internal partial class PlayerController : EntityController {
    #region SerializeField Variables
    [Header("Inputs")]
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference jump;
    [SerializeField] InputActionReference dash;
    [SerializeField] InputActionReference lightAttack;
    [SerializeField] InputActionReference heavyAttack;
    #endregion

    void OnEnable() {
        move.action.Enable();
        jump.action.Enable();
        dash.action.Enable();

        lightAttack.action.Enable();
        heavyAttack.action.Enable();
    }

    void OnDisable() {
        move.action.Disable();
        jump.action.Disable();
        dash.action.Disable();

        lightAttack.action.Disable();
        heavyAttack.action.Disable();
    }

    void StartInput() {
        jump.action.started += OnJump;
        jump.action.canceled += OnJump;

        dash.action.started += OnSprint;
        dash.action.canceled += OnSprint;

        lightAttack.action.started += TriggerAttack;
        heavyAttack.action.started += TriggerAttack;
    }
}
