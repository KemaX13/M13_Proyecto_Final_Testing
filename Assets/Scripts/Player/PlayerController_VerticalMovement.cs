using UnityEngine;
using UnityEngine.InputSystem;

internal partial class PlayerController : EntityController {
    #region SerializeField Variables
    [Header("Jump")]
    [SerializeField] int jumpUses = 1;
    [SerializeField] float maxJumpHeight = 2f;
    [SerializeField] float maxJumpTime = .5f;
    [SerializeField] float antiGravityTime = .2f;
    [SerializeField] float fallMulti = 2f;
    [SerializeField] string jumpableWall;
    #endregion

    #region Private Variables
    int remainingJump;
    float innitJumpVel;
    float antiGravitycurrentTime;
    float innitialPoint;
    bool isJumpPressed;
    bool isTouchingWall;
    #endregion

    void StartJump() {
        remainingJump = jumpUses;

        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        innitJumpVel = (2 * maxJumpHeight) / timeToApex;
    }

    void UpdateVerticalMovement() {
        if(!jump.action.WasPerformedThisFrame() || remainingJump <= 0)
            return;

        remainingJump--;
        antiGravitycurrentTime = 0;
        innitialPoint = transform.position.y;
        currentVel.y = innitJumpVel;
        appliedVel.y = innitJumpVel;
    }

    public void Gravity() {
        if(cc.isGrounded || (cc.collisionFlags == CollisionFlags.Sides && isTouchingWall)) {
            currentVel.y = gravityPercentage;
            appliedVel.y = gravityPercentage;
            remainingJump = jumpUses;
        } else if(transform.position.y >= maxJumpHeight + innitialPoint && antiGravitycurrentTime <= antiGravityTime && isJumpPressed) {
            currentVel.y = 0;
            appliedVel.y = 0;
            antiGravitycurrentTime += Time.deltaTime;
        } else {
            float tempGravity = gravity * Time.deltaTime;
            float previousYVel = currentVel.y;

            if(currentVel.y <= 0 || !isJumpPressed) currentVel.y += tempGravity * fallMulti;
            else currentVel.y += tempGravity;

            appliedVel.y = Mathf.Max((previousYVel + currentVel.y) * 0.5f, -20f);
        }
    }

    void OnJump(InputAction.CallbackContext input) {
        isJumpPressed = input.ReadValueAsButton();
    }
}
