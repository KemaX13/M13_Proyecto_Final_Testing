using UnityEngine;

internal abstract class EnemyController : EntityController {
    #region SerializeField Variables
    [Header("Detect Player")]
    [SerializeField] string playerTag = "Player";
    [SerializeField] float attackRadius = 1f;
    [SerializeField] FindObject playerTransform;
    #endregion

    #region Private Variables
    Vector3 dir;
    Transform target;
    #endregion

    protected override void Start() {
        base.Start();
        target = playerTransform.Value;
    }

    void Update() {
        UpdatePlaneMovement();

        Movement();
        Orientation();

        //UpdateCombat();
    }

    void UpdatePlaneMovement() {
        dir = (target.position - transform.position).normalized;
        
        appliedVel.x = currentVel.x = dir.x * velocity;
        appliedVel.z = currentVel.z = dir.z * velocity;
    }

    internal abstract void UpdateCombat();
}
