using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapSelectButton : MonoBehaviour
{
    // badmap id = 1
    // goodmap id = 2
    public void OnClick(int id) {
        SceneManager.LoadScene(id);
    }
}
