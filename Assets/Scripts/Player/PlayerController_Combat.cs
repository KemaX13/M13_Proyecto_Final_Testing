using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

internal partial class PlayerController : EntityController {
    #region SerializeField Variables
    [Header("Attack Type")]
    [SerializeField] string lightBoolName;
    [SerializeField] string heavyBoolName;
    #endregion
    
    protected override void UpdateCombat() {
        anim.SetAnimBool(lightBoolName, lightAttack.action.WasPerformedThisFrame() && anim.canAction);
        anim.SetAnimBool(heavyBoolName, heavyAttack.action.WasPerformedThisFrame() && anim.canAction);
    }

    void TriggerAttack(InputAction.CallbackContext input) {
        anim.SetAnimTrigger(attackTriggerName);
    }
}
