using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CharacterSelectionScript : MonoBehaviour 
{
    public Image Background;
    public Text CharacterChosen;
    public Text CharacterSelectionLabel;
    public Button Cure;
    public Button Disease;
    public Button StartButton;
	// Use this for initialization
	void Start () 
    {
        CharacterChosen.enabled = false;
	}
    public void CureSelected()
    {
        Debug.Log("Cure Selected");
        Background.color = Color.blue;
        CharacterSelectionLabel.enabled = false;
        CharacterChosen.text = "role : cure";
        CharacterChosen.enabled = true;
        //StartButton.guiText.text = "CURE!";
    }
    public void DiseaseSelected()
    {
        Debug.Log("Disease Selected");
        Background.color = Color.red;
        CharacterSelectionLabel.enabled = false;
        CharacterChosen.text = "role : disease";
        CharacterChosen.enabled = true;
        //StartButton.guiText.text = "INFECT!";
    }
    public void StartGameSelected()
    {
        Debug.Log("Starts Selected");
        // Find a way to pass the character selected
        Application.LoadLevel("Brain");
    }
}
