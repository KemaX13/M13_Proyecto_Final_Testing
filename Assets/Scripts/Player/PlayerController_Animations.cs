using UnityEngine;

internal partial class PlayerController : EntityController {
    #region SerializeField Variables
    [Header("Animations")]
    [SerializeField] string velocityName;
    [SerializeField] string verticalVelocityName;
    [SerializeField] string jumpTriggerName;
    #endregion

    #region Private Variables
    Animator anim;
    #endregion

    void StartAnimations() {
        anim = GetComponentInChildren<Animator>();
    }

    void UpdateAnim() {
        anim.SetFloat(velocityName, Mathf.Abs(appliedVel.x) + Mathf.Abs(appliedVel.z));
        anim.SetFloat(verticalVelocityName, cc.isGrounded ? appliedVel.y : 0);
    }
}
