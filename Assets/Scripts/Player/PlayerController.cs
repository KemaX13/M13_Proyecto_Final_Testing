using UnityEngine;
using UnityEngine.InputSystem;

internal class PlayerController : CharactersController {
    #region SerializeField Variables
    [Header("Jump")]
    [SerializeField] float jumpForce = 5f;
    [SerializeField] int jumpUses = 1;

    [Header("Inputs")]
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference jump;
    #endregion

    #region Private Variables
    Camera mainCamera;

    float verticalVel;
    int jumpCoins;
    #endregion

    protected override void Awake() {
        base.Awake();
        mainCamera = Camera.main;
    }

    protected override void Start() {
        base.Start();
        jumpCoins = jumpUses;
    }

    void OnEnable() {
        move.action.Enable();
        jump.action.Enable();
    }

    void OnDisable() {
        move.action.Disable();
        jump.action.Disable();
    }

    void FixedUpdate() {
        UpdatePlaneMovement();
        //UpdateVerticalMovement();

        Movement();

        Orientation();
        //Gravity();
    }

    void UpdatePlaneMovement() {
        Vector2 dir = move.action.ReadValue<Vector2>();

        Vector3 forward = mainCamera.transform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 moveXZ = forward * dir.y + mainCamera.transform.right.normalized * dir.x;
        currentVel = moveXZ * velocity;
    }

    void UpdateVerticalMovement() {
        if(cc.isGrounded) {
            verticalVel = 0;
            jumpCoins = jumpUses;
        }

        if(jump.action.WasPerformedThisFrame() && jumpCoins >= 0) {
            verticalVel = jumpForce;
            jumpCoins--;
        }
    }
}
