using UnityEngine;

internal abstract partial class EntityController : MonoBehaviour {
    #region SerializeField Variables
    [Header("Movement")]
    [SerializeField] protected float velocity = 5f;

    [Header("Orientation")]
    [SerializeField] protected float velocityRotation = 4f;

    [Header("Animations")]
    [SerializeField] protected string velocityName;
    [SerializeField] protected string verticalVelocityName;
    [SerializeField] protected string attackTriggerName;
    #endregion

    #region Private Variables
    protected CharacterController cc;
    protected AnimationController anim;

    protected Vector3 currentVel; //Direction
    protected Vector3 appliedVel;

    float angVel; //Angular velocity
    #endregion

    protected virtual void Awake() {
        cc = GetComponent<CharacterController>();
    }

    protected virtual void Start() {
        angVel = 360 * velocityRotation;

        anim = GetComponentInChildren<AnimationController>();
    }

    #region Movement
    protected void Movement() {
        cc.Move(appliedVel * Time.deltaTime);
        //anim.SetAnimValue(velocityName, Mathf.Abs(appliedVel.x) + Mathf.Abs(appliedVel.z));
    }

    protected void Orientation() {
        Vector3 desiredOrientation = currentVel;

        desiredOrientation.y = 0f;
        desiredOrientation.Normalize();

        float angDist = Vector3.SignedAngle(transform.forward, desiredOrientation, Vector3.up);

        float angBcsSpeed = angVel * Time.fixedDeltaTime;
        float remainAng = Mathf.Abs(angDist);

        float ang = Mathf.Sign(angDist) * Mathf.Min(angBcsSpeed, remainAng);

        Quaternion rotToApply = Quaternion.AngleAxis(ang, Vector3.up);

        transform.rotation = rotToApply * transform.rotation;
    }
    #endregion
}
