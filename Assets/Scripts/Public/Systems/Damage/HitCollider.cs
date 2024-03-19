using UnityEngine;
using UnityEngine.Events;

internal class HitCollider : MonoBehaviour {
    [SerializeField] UnityEvent<HitterCollider, HitCollider> onHitReceived;

    internal void NotifyHit(HitterCollider hitterCollider) {
        onHitReceived.Invoke(hitterCollider, this);
    }
}

