using UnityEngine;
using FSM;

internal abstract class CharactersController : MonoBehaviour {
    #region SerializeField Variables
    [Header("Movement")]
    [SerializeField] protected float velocity = 5f;
    [Range(0, 100), SerializeField] int gravityMul = 5;

    [Header("Orientation")]
    [SerializeField] protected float velocityRot = 4f; //Velocity rotation
    #endregion

    #region Private Variables
    protected CharacterController cc;

    protected Vector3 currentVel; //Direction
    protected Vector3 appliedVel;

    protected float gravity;
    protected float gravityPercentage;

    float angVel; //Angular velocity
    #endregion

    protected virtual void Awake() {
        cc = GetComponent<CharacterController>();
    }

    protected virtual void Start() {
        gravity = Physics.gravity.y;
        angVel = 360 * velocityRot;

        gravityPercentage = gravityMul * gravity / 100;
    }

    public void Movement() {
        cc.Move(appliedVel * Time.deltaTime);
    }

    public void Orientation() {
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
}
