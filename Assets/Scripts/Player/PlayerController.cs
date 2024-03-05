using UnityEngine;

internal partial class PlayerController : CharactersController {
    protected override void Awake() {
        base.Awake();

        mainCamera = Camera.main;
    }

    protected override void Start() {
        base.Start();

        StartInput();
        StartDash();
        StartJump();
    }

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

    void Update() {
        Dash();
        UpdatePlaneMovement();

        Movement();
        Orientation();

        Gravity();
        UpdateVerticalMovement();
    }
}
