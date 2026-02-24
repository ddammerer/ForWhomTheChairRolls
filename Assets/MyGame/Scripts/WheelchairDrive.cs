using UnityEngine;

public class WheelchairDrive : MonoBehaviour
{
    public float wheelRadius = 0.3f;   // meters
    public float wheelBase = 0.6f;     // distance between wheels

    int lastLeft;
    int lastRight;

    void Update()
    {
        // 🔹 Read directly from Arduino script
        int rightWheelDegrees = ArduinoSerialDebug.encoder1Value;
        int leftWheelDegrees  = ArduinoSerialDebug.encoder2Value;

        int dLeft = leftWheelDegrees - lastLeft;
        int dRight = rightWheelDegrees - lastRight;

        lastLeft = leftWheelDegrees;
        lastRight = rightWheelDegrees;

        float leftDist = Mathf.Deg2Rad * dLeft * wheelRadius;
        float rightDist = Mathf.Deg2Rad * dRight * wheelRadius;

        float forward = (leftDist + rightDist) / 2f;
        float rotation = (rightDist - leftDist) / wheelBase;

        transform.Translate(Vector3.forward * forward, Space.Self);
        transform.Rotate(Vector3.up, rotation * Mathf.Rad2Deg);
    }
}