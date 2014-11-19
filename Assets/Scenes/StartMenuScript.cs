using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartMenuScript : MonoBehaviour 
{
    public void StartGameButtonClicked()
    {
        Debug.Log("Start Clicked");
        Application.LoadLevel("CharacterSelectionMenu");
    }
    public void ControlsButtonClicked()
    {
        Debug.Log("Controls Clicked");
        Application.LoadLevel("ControlsMenu");
    }
    public void QuitButtonClicked()
    {
        Debug.Log("Quit Clicked");
        Application.Quit();
    }
}
