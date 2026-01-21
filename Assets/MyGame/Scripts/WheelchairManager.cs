using UnityEngine;

public class WheelchairManager : MonoBehaviour
{
    public GameObject chair;
    public GameObject rig;
    Vector3 offset;
    private void Start() {
        offset = new Vector3(0f, 0.1f, 0f);
    }

    private void Update() {
        chair.transform.localPosition = rig.transform.localPosition + offset;
        chair.transform.localRotation = Quaternion.Euler(0f, rig.transform.localEulerAngles.y + 180f, 0f);
    }
}
