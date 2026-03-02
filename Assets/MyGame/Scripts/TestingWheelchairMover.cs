using UnityEngine;

public class TestingWheelchairMover : MonoBehaviour {
    public GameObject wheelL;
    public GameObject wheelR;
    public float rotationSpeedR;
    public float rotationSpeedL;
    public GameObject BigParent;
    private ArduinoSerialDebug arduino;

    private void Awake()
    {
        arduino = BigParent.GetComponent<ArduinoSerialDebug>();
    }

    void Update() {

        rotationSpeedR = ArduinoSerialDebug.encoder1Value;
        rotationSpeedL = ArduinoSerialDebug.encoder2Value;
        RotateWheel(rotationSpeedR, wheelR);
        RotateWheel(rotationSpeedL, wheelL);
    }

    private void RotateWheel(float speed, GameObject wheel) {
        wheel.transform.Rotate(Vector3.right * speed * Time.deltaTime);
    }
}
