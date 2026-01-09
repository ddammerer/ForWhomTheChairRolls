using UnityEngine;
using UnityEngine.XR;

public class WheelchairController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 1.5f;
    public float turnSpeed = 60f;

    [Header("XR Controller")]
    public XRNode controllerNode = XRNode.LeftHand;

    private Rigidbody rb;
    private InputDevice controller;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // verhindert Umkippen

        controller = InputDevices.GetDeviceAtXRNode(controllerNode);
    }

    void FixedUpdate()
    {
        if (!controller.isValid)
        {
            controller = InputDevices.GetDeviceAtXRNode(controllerNode);
            return;
        }

        // Thumbstick auslesen
        if (controller.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 input))
        {
            // Vorwärts / Rückwärts
            Vector3 move = transform.forward * input.y * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + move);

            // Drehen
            float turn = input.x * turnSpeed * Time.fixedDeltaTime;
            Quaternion rotation = Quaternion.Euler(0f, turn, 0f);
            rb.MoveRotation(rb.rotation * rotation);
        }
    }
}
