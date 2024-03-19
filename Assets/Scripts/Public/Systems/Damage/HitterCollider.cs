using System.Linq;
using UnityEngine;
using UnityEngine.Events;

internal class HitterCollider : MonoBehaviour {
    [SerializeField] string[] hittableTags;
    [SerializeField] UnityEvent<HitterCollider, HitCollider> onHitDelivered;

    void OnTriggerEnter(Collider other) => OnHit(other);

    void OnCollisionEnter(Collision collision) => OnHit(collision.collider);

    void OnHit(Collider other) {
        HitCollider hC = other.GetComponent<HitCollider>();

        if(hC != null && CheckTag(other)) {
            hC.NotifyHit(this);
            onHitDelivered.Invoke(this, hC);
        }
    }

    bool CheckTag(Collider other) {
        return hittableTags.Contains(other.tag);
    }
}
