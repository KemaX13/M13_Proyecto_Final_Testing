using UnityEngine;

internal class AnimationController : MonoBehaviour {
    #region Private Params
    Animator anim;

    internal bool canAction = true;
    #endregion

    void Awake() {
        anim = GetComponent<Animator>();
    }

    internal void SetAnimValue(string name, float value) {
        anim.SetFloat(name, value);
    }

    internal void SetAnimTrigger(string name) {
        anim.SetTrigger(name);
    }

    internal void SetAnimBool(string name, bool isTrue) {
        anim.SetBool(name, isTrue);
    }

    public void CanAction(int value) {
        canAction = value != 0;
    }
}
