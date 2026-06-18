using UnityEngine;

public class WheelchairDrive : MonoBehaviour {
    public float wheelRadius = 0.3f;   // meters
    public float wheelBase = 0.6f;     // distance between wheels

    int lastLeft;
    int lastRight;

    void Update() {
        int rightWheelDegrees = ArduinoSerialDebug.encoder1Value;
        int leftWheelDegrees = ArduinoSerialDebug.encoder2Value;

        int dLeft = leftWheelDegrees - lastLeft;
        int dRight = rightWheelDegrees - lastRight;

        lastLeft = leftWheelDegrees;
        lastRight = rightWheelDegrees;

        float leftDist = Mathf.Deg2Rad * dLeft * wheelRadius;
        float rightDist = Mathf.Deg2Rad * dRight * wheelRadius;

        float forward = (leftDist + rightDist) / 2f;
        float rotation = (rightDist - leftDist) / wheelBase;

        Vector3 horizontalForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
        if (horizontalForward.sqrMagnitude < 0.0001f)
            horizontalForward = Vector3.forward;

        transform.position += horizontalForward * forward;
        transform.Rotate(Vector3.up, rotation * Mathf.Rad2Deg, Space.World);
    }
}

