using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuCanvas;
    public float distanceFromPlayer = 1.5f;
    public NearFarInteractor interactorLeft;
    public NearFarInteractor interactorRight;

    private bool isPaused = false; 
    Transform xrCamera;

    private void Start() => xrCamera = Camera.main.transform;

    void Update()
    {
        // Check for menu button (left controller menu button)
        if (CheckMenuButton() || Input.GetKeyDown(KeyCode.Escape))
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
        SetNearFarInteractors(true);
        // Nur horizontale Blickrichtung verwenden (Y ignorieren)
        Vector3 flatForward = xrCamera.forward;
        flatForward.y = 0f;
        flatForward.Normalize();

        // Position immer gerade vor dem Spieler, auf fixer Höhe
        Vector3 spawnPos = xrCamera.position + flatForward * distanceFromPlayer;
        spawnPos.y -= 0.1f;
        pauseMenuCanvas.transform.position = spawnPos;

        // Menu zum Spieler ausrichten (aufrecht, nicht gekippt)
        pauseMenuCanvas.transform.rotation = Quaternion.LookRotation(flatForward);

        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    void ClosePauseMenu()
    {
        SetNearFarInteractors(false);
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

    private void SetNearFarInteractors(bool toSet)
    {
        interactorLeft.enableFarCasting = toSet;
        interactorRight.enableFarCasting = toSet;
    }
}