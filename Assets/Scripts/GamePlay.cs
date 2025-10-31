using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlay : MonoBehaviour
{

    public GameObject PauseMenu;

   
       
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void RestartGame()
    {
        Time.timeScale = 1f;
        CancelInvoke(); // for the error of  'GameManager' has been destroyed but you are still trying to access it
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ExitGame()
    {
        Application.Quit();
        SceneManager.LoadScene("Menu");
    }


    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    void OnDestroy()
    {
      CancelInvoke();
    }
}
