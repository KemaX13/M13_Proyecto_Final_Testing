using UnityEngine;
using UnityEngine.InputSystem;

internal partial class PlayerController : EntityController {
    #region SerializeField Variables
    [Header("Gravity")]
    [Range(0, 100), SerializeField] int gravityPercentage = 5;

    [Header("Jump")]
    [SerializeField] int jumpUses = 1;
    [SerializeField] float maxJumpHeight = 2f;
    [SerializeField] float maxJumpTime = .5f;
    [SerializeField] float antiGravityTime = .2f;
    [SerializeField] float fallMultiplier = 2f;
    [SerializeField] string jumpTriggerName;

    [Header("Wall Collision")]
    [SerializeField] string jumpableWall;
    [SerializeField] float wallTimeAttach;
    [SerializeField] float wallValueMultiplier;
    [SerializeField] float capsuleRadius;
    [SerializeField] LayerMask jumpableWallMask;
    #endregion

    #region Private Variables
    float gravity;
    float gravityGrounded;

    int remainingJump;
    float innitJumpVel;
    float antiGravitycurrentTime;
    float innitialPoint;
    float maxJumpHeightRelative;

    bool isJumpPressed;
    bool isWallColliding;
    float wallCurrentTimeAttach;
    Vector3 lowPoint;
    Vector3 highPoint;
    #endregion

    void StartJump() {
        gravity = Physics.gravity.y;
        gravityGrounded = gravityPercentage * gravity / 100;

        remainingJump = jumpUses;

        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        innitJumpVel = (2 * maxJumpHeight) / timeToApex;

        lowPoint = Vector3.up * (cc.height / 4);
        highPoint = Vector3.up * ((cc.height * 3) / 4);
    }

    void UpdateVerticalMovement() {
        if(!jump.action.WasPerformedThisFrame() || remainingJump <= 0)
            return;

        remainingJump--;
        antiGravitycurrentTime = wallCurrentTimeAttach = 0;
        innitialPoint = transform.position.y;
        maxJumpHeightRelative = isWallColliding ? maxJumpHeight * wallValueMultiplier : maxJumpHeight;
        currentVel.y = appliedVel.y = isWallColliding ? innitJumpVel * wallValueMultiplier : innitJumpVel;
        isWallColliding = false;
        anim.SetAnimTrigger(jumpTriggerName);
    }

    public void Gravity() {
        Vector3 currentPos = transform.position;
        Collider[] colliders = Physics.OverlapCapsule(currentPos + lowPoint, currentPos + highPoint, capsuleRadius, jumpableWallMask);

        if(cc.isGrounded) {
            currentVel.y = appliedVel.y = gravityGrounded;
            remainingJump = jumpUses;
        } else if(isWallColliding && wallCurrentTimeAttach <= wallTimeAttach && colliders.Length > 0) {
            currentVel.y = appliedVel.y = 0;
            remainingJump = jumpUses;
            wallCurrentTimeAttach += Time.deltaTime;
        } else if(currentPos.y >= maxJumpHeightRelative + innitialPoint && antiGravitycurrentTime <= antiGravityTime && isJumpPressed) {
            currentVel.y = appliedVel.y = 0;
            antiGravitycurrentTime += Time.deltaTime;
        } else {
            float tempGravity = gravity * Time.deltaTime;
            float previousYVel = currentVel.y;

            if(currentVel.y <= 0 || !isJumpPressed) currentVel.y += tempGravity * fallMultiplier;
            else currentVel.y += tempGravity;

            appliedVel.y = Mathf.Max((previousYVel + currentVel.y) * 0.5f, -20f);
        }

        anim.SetAnimValue(verticalVelocityName, cc.isGrounded ? 0 : appliedVel.y);
    }

    void OnJump(InputAction.CallbackContext input) {
        isJumpPressed = input.ReadValueAsButton();
    }

    void OnControllerColliderHit(ControllerColliderHit hit) {
        isWallColliding = hit.collider.gameObject.tag == jumpableWall && cc.collisionFlags == CollisionFlags.Sides;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + lowPoint, capsuleRadius);
        Gizmos.DrawWireSphere(transform.position + highPoint, capsuleRadius);
    }
}
