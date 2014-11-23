using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CharacterSelectionScript : MonoBehaviour 
{
    const int DISEASE = 0;
    const int CURE = 1;

    public Image Background;
    public Text CharacterChosen;
    public Text CharacterSelectionLabel;
    public Button Cure;
    public Button Disease;
    public Text DiseaseText;
    public Text CureText;
    public Text BattleCountDownText;
    bool TimerStart = false;
    float Timer;

	// Use this for initialization
	void Start () 
    {
        CharacterChosen.enabled = false;
        BattleCountDownText.text = "";
	}
    public void CureSelected()
    {
        Background.color = Color.blue;
        CharacterSelectionLabel.enabled = false;
        CharacterChosen.text = "role : cure";
        CharacterChosen.enabled = true;

        // Hide Buttons
        Disease.image.enabled = false;
        DiseaseText.text = "";
        Cure.image.enabled = false;
        CureText.text = "";

        Timer = 0.0f;
        BattleCountDownText.text = "match start in: 3";
        TimerStart = true;
        GameManager.SetPlayerSelected(CURE);
    }
    public void DiseaseSelected()
    {
        Background.color = Color.red;
        CharacterSelectionLabel.enabled = false;
        CharacterChosen.text = "role : disease";
        CharacterChosen.enabled = true;

        // Hide Buttons
        Cure.image.enabled = false;
        CureText.text = "";
        Disease.image.enabled = false;
        DiseaseText.text = "";

        Timer = 0.0f;
        BattleCountDownText.text = "match start in: 3";
        TimerStart = true;
        GameManager.SetPlayerSelected(DISEASE);
    }
    void Update()
    {
        if(TimerStart)
        {
            Timer += Time.deltaTime;
            if(Timer >= 3.0f)
            {
                Application.LoadLevel("Brain");
            }
            else if(Timer >= 2.0f)
            {
                BattleCountDownText.text = "match start in : 1";
            }
            else if (Timer >= 1.0f)
            {
                BattleCountDownText.text = "match start in : 2";
            }
        }
    }
}
