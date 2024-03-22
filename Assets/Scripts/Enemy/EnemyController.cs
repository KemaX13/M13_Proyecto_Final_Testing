using UnityEngine;

internal abstract class EnemyController : EntityController {
    #region SerializeField Variables
    [Header("Detect Player")]
    //[SerializeField] string playerTag = "Player";
    [SerializeField] protected float[] attackRadiuses;
    [SerializeField] FindObject playerTransform;

    [Header("Combat")]
    [SerializeField] protected float attackTime = 3f;
    [SerializeField] Color[] lineColors;
    #endregion

    #region Private Variables
    Vector3 dir;
    Transform target;
    protected int attackIndex;

    protected bool canAttack;
    protected float attackCurrentTime;
    #endregion

    protected override void Start() {
        base.Start();

        target = playerTransform.Value;
        attackCurrentTime = attackTime;
    }

    void Update() {
        UpdatePlaneMovement();

        Movement();
        Orientation();

        UpdateCombat();
    }

    void UpdatePlaneMovement() {
        float distance = Vector3.Distance(transform.position, target.position);
        canAttack = distance < attackRadiuses[attackIndex];

        dir = canAttack && !anim.canAction ? (target.position - transform.position).normalized : Vector3.zero;

        appliedVel.x = currentVel.x = dir.x * velocity;
        appliedVel.z = currentVel.z = dir.z * velocity;
    }

    internal abstract void UpdateCombat();

    void OnDrawGizmos() {
        if(lineColors.Length != attackRadiuses.Length) {
            Debug.LogError("La cantidad de colores no coincide con la cantidad de radios de ataque.");
            return;
        }

        for(int i = 0; i < attackRadiuses.Length; i++) {
            Gizmos.color = lineColors[i];

            Vector3 endPosition = transform.position + transform.forward * attackRadiuses[i];
            Gizmos.DrawLine(transform.position, endPosition);
        }
    }
}
