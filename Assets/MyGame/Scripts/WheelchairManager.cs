using UnityEngine;

public class WheelchairManager : MonoBehaviour
{
    public GameObject chair;
    Camera cam;
    Vector3 offset;
    private void Start() {
        cam = Camera.main;
        offset = new Vector3(0.425f, -0.3f, 0f);
    }

    private void Update() {
        chair.transform.localPosition = cam.transform.localPosition + offset;
        chair.transform.localRotation = Quaternion.Euler(0f, cam.transform.localEulerAngles.y + 180f, 0f);
    }
}
