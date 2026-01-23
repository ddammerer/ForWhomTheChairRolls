using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class ResetPlayer : MonoBehaviour
{
    public Transform PlayerStartingPosition;

    private InputDevice rightHand;

    void Start()
    {
        transform.position = PlayerStartingPosition.position;
        rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    void Update()
    {
        if (!rightHand.isValid)
        {
            rightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            return;
        }

        if (rightHand.TryGetFeatureValue(CommonUsages.primaryButton, out bool pressed) && pressed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
