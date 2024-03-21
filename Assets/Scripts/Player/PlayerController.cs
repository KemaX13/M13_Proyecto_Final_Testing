using UnityEngine;

internal partial class PlayerController : EntityController {
    #region Private Variables
    [SerializeField] FindObject playerTransform;
    #endregion

    protected override void Awake() {
        base.Awake();

        mainCamera = Camera.main;
        playerTransform.Value = transform;
    }

    protected override void Start() {
        base.Start();

        StartInput();
        StartDash();
        StartJump();
    }

    void Update() {
        Dash();
        UpdatePlaneMovement();

        Movement();
        Orientation();

        Gravity();
        UpdateVerticalMovement();

        UpdateCombat();
    }
}
