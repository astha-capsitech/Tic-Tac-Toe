using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class MainManager : MonoBehaviour
{
    [Header("Sprites(Assign your X an O images)")]
    public Sprite xSprite;
    public Sprite oSprite;

    [Header("UI Buttons(Assign in Inspector)")]
    public Button xButton;
    public Button oButton;
  

    private string playerChoice;
    private string computerChoice;

    [Header("Panels(Assign in Inspector)")]
    public GameObject choicePanel;
    public GameObject bameGridPanel;

    public GameObject resultPanel;

    public GameObject pausePanel;

    

    void Start()
    {
        if (xButton != null && oButton != null)
        {
            xButton.onClick.AddListener(() => OnChoiceSelected("X"));
            oButton.onClick.AddListener(() => OnChoiceSelected("O"));
        }

         if (choicePanel != null)
        {
            choicePanel.SetActive(true);
        }
        // panel change 
        if (bameGridPanel != null)
        {
            bameGridPanel.SetActive(false);
        }
        if (resultPanel != null)
        {
            resultPanel.SetActive(false);
        }
        
        if(pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }
    


    void OnChoiceSelected(string choice)
    {
        playerChoice = choice;
        computerChoice = (choice == "X") ? "O" : "X";

        choicePanel.SetActive(false);
        bameGridPanel.SetActive(true);
        resultPanel.SetActive(false);
        pausePanel.SetActive(true);
      
      

        PlayerPrefs.SetString("PlayerChoice", playerChoice);
        PlayerPrefs.SetString("ComputerChoice", computerChoice);
        PlayerPrefs.Save();
        

    }

}
