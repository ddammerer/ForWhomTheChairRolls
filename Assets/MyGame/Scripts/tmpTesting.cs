using UnityEngine;
using UnityEngine.SceneManagement;

public class tmpTesting : MonoBehaviour
{
    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && 
            SceneManager.GetActiveScene().name != "LevelSelect")
            SceneManager.LoadSceneAsync("LevelSelect");
    }
}
