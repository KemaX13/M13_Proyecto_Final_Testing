using UnityEngine;
using UnityEngine.Events;

internal class ResetScript : MonoBehaviour {
    internal static UnityEvent resetEvent = new UnityEvent();

    public void ResetAll() => resetEvent.Invoke();
}
