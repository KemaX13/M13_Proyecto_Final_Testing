using UnityEngine;
using UnityEngine.InputSystem;

internal partial class PlayerController : CharactersController {
    #region SerializeField Variables
    [Header("Jump")]
    [SerializeField] int jumpUses = 1;
    [SerializeField] float maxJumpHeight = 2f;
    [SerializeField] float maxJumpTime = .5f;
    [SerializeField] float antiGravityTime = .2f;
    #endregion

    #region Private Variables
    int remainingJump;
    float innitJumpVel;
    float antiGravitycurrentTime;
    bool isJumping;
    bool isJumpPressed;
    #endregion

    void StartJump() {
        remainingJump = jumpUses;

        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        innitJumpVel = (2 * maxJumpHeight) / timeToApex;
    }

    void UpdateVerticalMovement() {
        if((!isJumping || remainingJump >= 0) && cc.isGrounded && isJumpPressed) {
            isJumping = true;
            remainingJump--;
            currentVel.y = innitJumpVel;
            appliedVel.y = innitJumpVel;
        } else if(!isJumpPressed && isJumping && cc.isGrounded) {
            isJumping = false;
        }
    }

    public void Gravity() {
        bool isFalling = currentVel.y <= 0 || !isJumpPressed;
        float fallMulti = 2f;

        if(cc.isGrounded) {
            currentVel.y = gravityPercentage; //velY = gravity * 5%
            appliedVel.y = gravityPercentage;
            antiGravitycurrentTime = 0;
            remainingJump = jumpUses;
        } else if(transform.position.y >= maxJumpHeight && antiGravitycurrentTime <= antiGravityTime && isJumpPressed) {
            currentVel.y = 0; //velY = gravity = 0
            appliedVel.y = 0;
            antiGravitycurrentTime += Time.deltaTime;
        } else if(isFalling) {
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
