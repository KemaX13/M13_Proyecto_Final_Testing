using UnityEngine;
using UnityEngine.InputSystem;

internal class PlayerController : CharactersController {
    #region SerializeField Variables
    [Header("Jump")]
    [SerializeField] int jumpUses = 1;
    [SerializeField] float maxJumpHeight = 2f;
    [SerializeField] float maxJumpTime = 1.2f;
    [SerializeField] float antiGravityTime = 0.6f;

    [Header("Inputs")]
    [SerializeField] InputActionReference move;
    [SerializeField] InputActionReference jump;
    #endregion

    #region Private Variables
    Camera mainCamera;
    Vector2 dir;

    int jumpCoins;
    float innitJumpVel;
    bool isJumping;
    bool isJumpPressed;

    float antiGravitycurrentTime;
    #endregion

    protected override void Awake() {
        base.Awake();
        mainCamera = Camera.main;
    }

    protected override void Start() {
        base.Start();

        jump.action.started += OnJump;
        jump.action.canceled += OnJump;

        jumpCoins = jumpUses;

        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        innitJumpVel = (2 * maxJumpHeight) / timeToApex;
    }

    void OnEnable() {
        move.action.Enable();
        jump.action.Enable();
    }

    void OnDisable() {
        move.action.Disable();
        jump.action.Disable();
    }

    void Update() {
        UpdatePlaneMovement();

        Movement();
        Orientation();

        Gravity();
        UpdateVerticalMovement();
    }

    void UpdatePlaneMovement() {
        dir = move.action.ReadValue<Vector2>();

        Vector3 forward = mainCamera.transform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 moveXZ = forward * dir.y + mainCamera.transform.right.normalized * dir.x;

        currentVel.x = moveXZ.x * velocity;
        currentVel.z = moveXZ.z * velocity;

        appliedVel.x = currentVel.x;
        appliedVel.z = currentVel.z;
    }

    void UpdateVerticalMovement() {
        if(!isJumping && cc.isGrounded && isJumpPressed) {
            isJumping = true;
            currentVel.y = innitJumpVel; 
            appliedVel.y = innitJumpVel;
        } else if (!isJumpPressed && isJumping && cc.isGrounded) {
            isJumping = false;
        }
    }

    void OnJump(InputAction.CallbackContext input) {
        isJumpPressed = input.ReadValueAsButton();
    }

    public void Gravity() {
        bool isFalling = currentVel.y <= 0 || !isJumpPressed;
        float fallMulti = 2f;
        
        if (cc.isGrounded) {
            currentVel.y = gravityPercentage;
            appliedVel.y = gravityPercentage;
            antiGravitycurrentTime = 0;
        } else if (transform.position.y >= maxJumpHeight && antiGravitycurrentTime <= antiGravityTime && isJumpPressed) {
            currentVel.y = 0;
            appliedVel.y = 0;
            antiGravitycurrentTime += Time.deltaTime;
        } else if(isFalling) {
            float previousYVel = currentVel.y;
            currentVel.y = currentVel.y + (gravity * fallMulti * Time.deltaTime);
            appliedVel.y = Mathf.Max((previousYVel + currentVel.y) * .5f, -20f);
        } else {
            float previousYVel = currentVel.y;
            currentVel.y = currentVel.y + (gravity * Time.deltaTime);
            appliedVel.y = (previousYVel + currentVel.y) * .5f;
        } 
    }
}
