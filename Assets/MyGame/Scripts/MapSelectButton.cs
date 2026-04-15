using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DropdownButton : MonoBehaviour
{
  public Dropdown dropdown;


 
    public void MapSelect()
    {
        string selectedValue = dropdown.options[dropdown.value].text;


        switch (selectedValue)
        {
            case "Good Map":
                SceneManager.LoadScene(2);
                break;

            case "Bad Map":
                SceneManager.LoadScene(1);

                break;

            default:
                Debug.LogWarning("Unbekannte Option: " + selectedValue);
                break;
        }
    }


}