using UnityEngine;
using UnityEngine.InputSystem;

internal partial class PlayerController : EntityController {
    #region SerializeField Variables
    [Header("Attack Type")]
    [SerializeField] string lightBoolName;
    [SerializeField] string heavyBoolName;
    [SerializeField] float responceTime;
    #endregion

    #region Private Params
    float attackCurrentTime;
    #endregion

    protected override void UpdateCombat() {
        attackCurrentTime -= Time.deltaTime;

        SetAnimBool(lightBoolName, lightAttack.action.WasPerformedThisFrame() && attackCurrentTime > 0); Debug.Log(lightAttack.action.WasPerformedThisFrame() && attackCurrentTime > 0);
        SetAnimBool(heavyBoolName, heavyAttack.action.WasPerformedThisFrame() && attackCurrentTime > 0);
    }

    void TriggerAttack(InputAction.CallbackContext input) {
        SetAnimTrigger(attackTriggerName);
        attackCurrentTime = responceTime;
    }
}
