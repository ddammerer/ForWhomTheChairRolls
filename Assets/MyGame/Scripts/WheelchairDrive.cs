using UnityEngine;

public class WheelchairDrive : MonoBehaviour
{
    public float wheelRadius = 0.3f;   // meters
    public float wheelBase = 0.6f;     // distance between wheels

    public RandImputScript inputScript;

    public int leftWheelDegrees;
    public int rightWheelDegrees;

    int lastLeft;
    int lastRight;


    void Update()
    {

        rightWheelDegrees = inputScript.valueA;
        leftWheelDegrees = inputScript.valueB;

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
