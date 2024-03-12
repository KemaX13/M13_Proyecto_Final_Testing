using UnityEngine;
using UnityEngine.InputSystem;

internal partial class PlayerController : EntityController {
    #region SerializeField Variables
    [Header("AttackType")]
    [SerializeField] protected string lightBoolName;
    [SerializeField] protected string heavyBoolName;
    #endregion

    #region Private Params
    float attackCurrentTime;
    #endregion

    protected override void Attack() {
        attackCurrentTime -= Time.deltaTime;

        //SetAnimBool(lightBoolName, lightAttackPerformed);
        //SetAnimBool(heavyBoolName, heavyAttackPerformed);
    }

    void LightAttack(InputAction.CallbackContext input) {
        SetAnimTrigger(attackTriggerName);
    }

    void HeavyAttack(InputAction.CallbackContext input) {
        SetAnimTrigger(attackTriggerName);
    }
}
