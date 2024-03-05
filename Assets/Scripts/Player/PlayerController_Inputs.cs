using UnityEngine;
using UnityEngine.InputSystem;

internal partial class PlayerController : CharactersController {
    #region SerializeField Variables
    [Header("Inputs")]
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference jump;
    [SerializeField] InputActionReference dash;
    #endregion

    void StartInput() {
        jump.action.started += OnJump;
        jump.action.canceled += OnJump;

        dash.action.started += OnSprint;
        dash.action.canceled += OnSprint;
    }
}
