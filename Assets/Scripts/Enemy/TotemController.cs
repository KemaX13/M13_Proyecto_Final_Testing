using UnityEngine;

internal class TotemController : EnemyController {
    internal override void UpdateCombat() {
        if(!anim.canAction)
            attackCurrentTime -= Time.deltaTime;

        if(anim.canAction || canAttack || attackCurrentTime > 0)
            return;

        attackIndex = Random.Range(0, attackRadiuses.Length);
        attackCurrentTime = attackTime;
    }
}
