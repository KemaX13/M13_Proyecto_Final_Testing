using UnityEngine;
using FSM;

internal class CharactersController : MonoBehaviour {
    #region SerializeField Variables
    [Header("States")]
    //[SerializeField] States[] states;

    [Header("Movement")]
    [SerializeField] protected float velocity = 4f;

    [Header("Orientation")]
    [SerializeField] protected float velocityRot = 4f; //Velocity rotation
    #endregion

    #region Private Variables
    protected CharacterController cc;

    protected Vector3 currentVel; //Direction

    float gravity;
    float gravityMul = 1f;

    float angVel; //Angular velocity
    #endregion

    protected virtual void Awake() {
        cc = GetComponent<CharacterController>();
    }

    protected virtual void Start() {
        gravity = Physics.gravity.y;
        angVel = 360 * velocityRot;
    }

    public void Movement() {
        cc.Move(currentVel * Time.fixedDeltaTime);
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

    public void Gravity() {
        if(!cc.isGrounded)
            cc.Move(Vector3.up * gravity * gravityMul * Time.fixedDeltaTime);
    }
}
