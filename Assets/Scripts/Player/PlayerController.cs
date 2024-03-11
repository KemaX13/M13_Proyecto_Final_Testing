using UnityEngine;

internal partial class PlayerController : EntityController {
    protected override void Awake() {
        base.Awake();

        mainCamera = Camera.main;
    }

    protected override void Start() {
        base.Start();

        StartInput();
        StartDash();
        StartJump();
        StartAnimations();
    }

    void Update() {
        Dash();
        UpdatePlaneMovement();

        Movement();
        Orientation();

        Gravity();
        UpdateVerticalMovement();

        UpdateAnim();
    }
}
