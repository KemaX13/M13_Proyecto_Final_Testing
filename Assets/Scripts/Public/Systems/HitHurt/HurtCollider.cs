using UnityEngine;
using UnityEngine.Events;

internal class HurtCollider : MonoBehaviour {
    [SerializeField] UnityEvent<HitCollider, HurtCollider> onHitReceived;

    internal void NotifyHit(HitCollider hitCollider) => onHitReceived.Invoke(hitCollider, this);
}
