using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverMenuScript : MonoBehaviour
{
    const int DISEASE = 0;
    const int CURE = 1;

    public Text PlayerWinText;
    public Button ReplayButton;
    public Button Quit;

    public Image Background;
    void Start()
    {
        if (GameManager.GameWinner == DISEASE)
        {
            PlayerWinText.text = "the disease wins!";
            Background.color = Color.red;
        }
        else if (GameManager.GameWinner == CURE)
        {
            PlayerWinText.text = "the cure wins!";
            Background.color = Color.blue;
        }
    }
    public void ReplayButtonClicked()
    {
        // Reset GameManager
        GameManager.SetGameWinner(-1);
        GameManager.SetPlayerSelected(-1);
        Application.LoadLevel("StartMenu");
    }
    public void QuitButtonClicked()
    {
        Application.Quit();
    }
}
