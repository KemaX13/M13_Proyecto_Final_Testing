using UnityEngine;

internal class HealthEnemyUI : HealthUI {
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;

    Camera m_camera;

    void Start() {
        m_camera = Camera.main;
    }

    void Update() {
        transform.rotation = m_camera.transform.rotation;
        transform.position = target.position + offset;
    }
}
