using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ResetPlayer : MonoBehaviour
{
    public Transform PlayerStartingPosition;

    void Start()
    {
        transform.position = PlayerStartingPosition.position;
    }

    void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
