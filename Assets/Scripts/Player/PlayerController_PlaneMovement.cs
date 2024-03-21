using UnityEngine;
using UnityEngine.InputSystem;

internal partial class PlayerController : EntityController {
    #region SerializeField Variables
    [Header("Dash")]
    [SerializeField] float maxDashLegth = 2f;
    [SerializeField] float maxDashTime = .7f;
    #endregion

    #region Private Variables
    Camera mainCamera;
    Vector2 dir;

    float acceleration;
    float innitDashSpeed;
    float finalDashSpeed;
    bool isDashing;
    bool isDashPressed;
    #endregion

    void StartDash() {
        innitDashSpeed = maxDashLegth / maxDashTime;
        acceleration -= innitDashSpeed / maxDashTime;
    }

    void UpdatePlaneMovement() {
        dir = move.action.ReadValue<Vector2>();

        Vector3 forward = mainCamera.transform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 moveXZ = forward * dir.y + mainCamera.transform.right.normalized * dir.x;
        finalDashSpeed = Mathf.Max(1f, (finalDashSpeed + acceleration * Time.deltaTime));

        appliedVel.x = currentVel.x = moveXZ.x * velocity * finalDashSpeed;
        appliedVel.z = currentVel.z = moveXZ.z * velocity * finalDashSpeed;
    }

    void Dash() {
        if(!isDashing && isDashPressed) {
            isDashing = true;
            finalDashSpeed = innitDashSpeed;
        } else if(!isDashPressed && isDashing) {
            finalDashSpeed = 1f;
            isDashing = false;
        }
    }

    void OnSprint(InputAction.CallbackContext input) {
        isDashPressed = input.ReadValueAsButton();
    }
}
