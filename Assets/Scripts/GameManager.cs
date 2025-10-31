using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Sprites(xSprite , osprite)")]
    public Sprite xSprite;
    public Sprite oSprite;

    [Header("grid")]
    public Button[] gridButtons;

    private string playerChoice;
    private string computerChoice;
    private bool playerTurn = true;

    [Header("result")]
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    private bool gameOver = false;

    public AudioClip clickSound;
    private AudioSource audioSource;

    public AudioClip failure;

    public AudioClip winner;

    public GameObject pausePanel;




    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        foreach (Button btn in gridButtons)
        {
            if (btn == null)
            {
                return;
            }
            btn.image.sprite = null;
            btn.onClick.RemoveAllListeners();
           btn.onClick.AddListener(() => onGridClicked(btn));
        }

        resultPanel.SetActive(false);
        pausePanel.SetActive(true);
        


    }
  
  // Player move 
    void onGridClicked(Button clickedButton)
    {
        
        playerChoice = PlayerPrefs.GetString("PlayerChoice");
        computerChoice = PlayerPrefs.GetString("computerChoice", (playerChoice == "X") ? "O" : "X");
        if (!playerTurn || gameOver)
        {
            return;
        }
        // symbolimage is  null or not
        Image symbolImg = GetSymbolImage(clickedButton);
        if (symbolImg == null)
        {
            return;
        }
        // cell already occupied 
        if (symbolImg.sprite != null)
        {
            return;
        }

        audioSource.PlayOneShot(clickSound);
        symbolImg.sprite = (playerChoice == "X") ? xSprite : oSprite;
        symbolImg.color = Color.white;

        if (CheckWinner(playerChoice))
        {
            CancelInvoke(nameof(ComputerMove));
            EndGame("Player Wins!");
            pausePanel.SetActive(false);
            audioSource.PlayOneShot(winner);
            return;
        }

        if (IsBoardFull())
        {
            EndGame("Draw!");
            audioSource.PlayOneShot(failure);
            pausePanel.SetActive(false);
            return;
        }
            playerTurn = false;
        Invoke(nameof(ComputerMove), 0.35f);
    }

    void ComputerMove()
    {
        if (gameOver) return;
        // getting the empty cell from the list of grid so that computer can play its turn
        List<Button> empty = new List<Button>();
        foreach (Button b in gridButtons)
        {
            Image img = GetSymbolImage(b);
            if (img != null && img.sprite == null)
            {
                empty.Add(b);
            }
        }
        // checks if the cells are left or not if there is no cell left then computer will not work
        if (empty.Count == 0)
        {
            return;
        }

        int r = Random.Range(0, empty.Count);
        Button chosen = empty[r];
        Image symbolImg = GetSymbolImage(chosen);
        if (symbolImg == null) return;
        audioSource.PlayOneShot(clickSound);
        symbolImg.sprite = (playerChoice == "X") ? oSprite : xSprite;
        symbolImg.color = Color.white;

        if (CheckWinner(computerChoice))
        {

            EndGame("Computer Wins!");
            pausePanel.SetActive(false);
            audioSource.PlayOneShot(winner);
            return;
        }
        
        if(IsBoardFull())
        {
            EndGame("Draw!");
            audioSource.PlayOneShot(failure);
             pausePanel.SetActive(false);
            return;
        }
        playerTurn = true;

    }
    // PROVIDE THE  SYMBOL OF IMAGE OF PLAYER AND COMPUTER
    Image GetSymbolImage(Button btn)
    {
        Transform child = btn.transform.Find("SymbolImage");
        if (child != null)
        {
            Image c = child.GetComponent<Image>();
            if (c != null) return c;
        }

        Image bimg = btn.GetComponent<Image>();
        if (bimg != null) return bimg;

        return null;
    }

     bool CheckWinner(string symbol)
    {
        Sprite checkSprite = (symbol == "X") ? xSprite : oSprite;
        int[,] winPatterns = new int[,]
        {
            {0,1,2}, {3,4,5}, {6,7,8},     
            {0,3,6}, {1,4,7}, {2,5,8},     
            {0,4,8}, {2,4,6}               
        };

        for (int i = 0; i < winPatterns.GetLength(0); i++)
        {
            int a = winPatterns[i, 0];
            int b = winPatterns[i, 1];
            int c = winPatterns[i, 2];

            Image imgA = GetSymbolImage(gridButtons[a]);
            Image imgB = GetSymbolImage(gridButtons[b]);
            Image imgC = GetSymbolImage(gridButtons[c]);

            if (imgA.sprite == checkSprite && imgB.sprite == checkSprite && imgC.sprite == checkSprite)
            {
                 
                imgA.color = Color.green;
                imgB.color = Color.green;
                imgC.color = Color.green;
                return true;
            }
           
           

        }
        return false;
    }

    bool IsBoardFull()
    {
        foreach (Button btn in gridButtons)
        {
            Image img = GetSymbolImage(btn);
            if (img != null && img.sprite == null)
                return false;
        }
        return true;
    }

    void EndGame(string message)
    {
        gameOver = true;
        resultText.text = message;
        resultPanel.SetActive(true);
        //  button can't clicked after end of the game
        foreach (Button b in gridButtons)
            b.interactable = false;

        
    }
    
  
}


