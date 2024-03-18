using System.Linq;
using UnityEngine;
using UnityEngine.Events;

internal class HitCollider : MonoBehaviour {
    [SerializeField] string[] hittableTags;
    [SerializeField] UnityEvent<HitCollider, HurtCollider> onHitDelivered;

    void OnTriggerEnter(Collider other) => OnHit(other);

    void OnCollisionEnter(Collision collision) => OnHit(collision.collider);

    void OnHit(Collider other) {
        HurtCollider hC = other.GetComponent<HurtCollider>();

        if(hC != null && CheckTag(other)) {
            hC.NotifyHit(this);
            onHitDelivered.Invoke(this, hC);
        }
    }

    bool CheckTag(Collider other) {
        return hittableTags.Contains(other.tag);
    }
}
