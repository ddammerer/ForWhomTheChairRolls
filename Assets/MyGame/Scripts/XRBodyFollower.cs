using UnityEngine;
using Unity.XR.CoreUtils;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class XRBodyFollower : MonoBehaviour {
    public XROrigin xrOrigin;
    public float followSpeed = 5f;

    Rigidbody rb;
    CapsuleCollider col;

    void Awake() {
        rb = xrOrigin.Origin.GetComponent<Rigidbody>();
        col = xrOrigin.Origin.GetComponent<CapsuleCollider>();
    }

    void FixedUpdate() {
        // Head position in world space
        Vector3 headPos = xrOrigin.Camera.transform.position;

        // Body position (collider center)
        Vector3 bodyPos = rb.position;

        // Horizontal difference only
        Vector3 delta = headPos - bodyPos;
        delta.y = 0f;

        // Allow head to move inside capsule radius
        float radius = col.radius * 0.9f;

        if (delta.magnitude > radius) {
            Vector3 target = bodyPos + delta.normalized * (delta.magnitude - radius);
            rb.MovePosition(Vector3.MoveTowards(bodyPos, target, followSpeed * Time.fixedDeltaTime));
        }
    }
}
