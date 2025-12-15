using UnityEngine;

public class TestingWheelchairMover : MonoBehaviour {
    public KeyCode moveWheelForwardButton;
    public KeyCode moveWheelBackwardButton;
    public GameObject wheel;
    public float rotationSpeed = 200f;

    void Update() {
        if (Input.GetKey(moveWheelForwardButton)) {
            RotateWheel(rotationSpeed);
        }

        if (Input.GetKey(moveWheelBackwardButton)) {
            RotateWheel(-rotationSpeed);
        }
    }

    private void RotateWheel(float speed) {
        wheel.transform.Rotate(Vector3.right * speed * Time.deltaTime);
    }
}
