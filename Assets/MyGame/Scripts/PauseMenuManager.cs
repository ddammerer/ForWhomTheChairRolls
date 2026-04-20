using UnityEngine;
using UnityEngine.XR;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuCanvas;
    public Transform xrCamera; // Your XR camera (Main Camera under XR Origin)
    public float distanceFromPlayer = 1.5f;

    private bool isPaused = false;

    void Update()
    {
        // Check for menu button (left controller menu button)
        //if (CheckMenuButton())
        //    TogglePause();

        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    bool CheckMenuButton()
    {
        InputDevice leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        bool menuPressed;
        if (leftController.TryGetFeatureValue(CommonUsages.menuButton, out menuPressed) && menuPressed)
        {
            return true;
        }
        return false;
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
            OpenPauseMenu();
        else
            ClosePauseMenu();
    }

    void OpenPauseMenu()
    {
        // Position menu in front of player's view
        Vector3 spawnPos = xrCamera.position + xrCamera.forward * distanceFromPlayer;
        pauseMenuCanvas.transform.position = spawnPos;

        // Face the player
        pauseMenuCanvas.transform.LookAt(xrCamera.position);
        pauseMenuCanvas.transform.Rotate(0, 180f, 0); // Flip to face correctly

        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    void ClosePauseMenu()
    {
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    // Called by Resume button
    public void OnResumePressed() => TogglePause();

    // Called by Quit button
    public void OnQuitPressed()
    {
        Time.timeScale = 1f;
        // UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        Application.Quit();
    }
}