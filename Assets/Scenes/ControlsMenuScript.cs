using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlsMenuScript : MonoBehaviour 
{
    public Button BackButton;
    public void BackButtonClicked()
    {
        Application.LoadLevel("StartMenu");
    }
}
