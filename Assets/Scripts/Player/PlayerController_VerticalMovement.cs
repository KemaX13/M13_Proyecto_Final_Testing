using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

internal partial class PlayerController : EntityController {
    #region SerializeField Variables
    [Header("Jump")]
    [SerializeField] int jumpUses = 1;
    [SerializeField] float maxJumpHeight = 2f;
    [SerializeField] float maxJumpTime = .5f;
    [SerializeField] float antiGravityTime = .2f;
    [SerializeField] float fallMulti = 2f;
    #endregion

    #region Private Variables
    int remainingJump;
    float innitJumpVel;
    float antiGravitycurrentTime;
    float innitialPoint;
    bool isJumpPressed;
    #endregion

    void StartJump() {
        remainingJump = jumpUses;

        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        innitJumpVel = (2 * maxJumpHeight) / timeToApex;
    }

    void UpdateVerticalMovement() {
        if(jump.action.WasPerformedThisFrame() && remainingJump > 0) {
            remainingJump--;
            antiGravitycurrentTime = 0;
            innitialPoint = transform.position.y;
            currentVel.y = innitJumpVel;
            appliedVel.y = innitJumpVel;
        }
    }

    public void Gravity() {
        if(cc.isGrounded) {
            currentVel.y = gravityPercentage; //velY = gravity * 5%
            appliedVel.y = gravityPercentage;
            remainingJump = jumpUses;
        } else if(transform.position.y >= maxJumpHeight + innitialPoint && antiGravitycurrentTime <= antiGravityTime && isJumpPressed) {
            currentVel.y = 0; //velY = gravity = 0
            appliedVel.y = 0;
            antiGravitycurrentTime += Time.deltaTime;
        } else if(currentVel.y <= 0 || !isJumpPressed) {
            float previousYVel = currentVel.y;
            currentVel.y = currentVel.y + (gravity * fallMulti * Time.deltaTime); //velY = gravity * ?% * tiempo
            appliedVel.y = Mathf.Max((previousYVel + currentVel.y) * .5f, -20f);
        } else {
            float previousYVel = currentVel.y;
            currentVel.y = currentVel.y + (gravity * Time.deltaTime);
            appliedVel.y = (previousYVel + currentVel.y) * .5f;
        }
    }

    void OnJump(InputAction.CallbackContext input) {
        isJumpPressed = input.ReadValueAsButton();
    }
}
