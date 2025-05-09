using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject DeathScreenUI;
    public GameObject PauseMenuUI;

    private void Update()
    {
        if (PauseMenuUI.activeSelf){
            Time.timeScale = 0f;
        }
        else{
            Time.timeScale = 1f;
        }
    }
    public void PauseMenu()
    {
        if(PauseMenuUI.activeSelf || DeathScreenUI.activeSelf)
        {
            PauseMenuUI.SetActive(false);
        }
        else
        {
            PauseMenuUI.SetActive(true);
        }
    }

    public void Deathscreen()
    {
        DeathScreenUI.SetActive(true);
    }
    
    public void RestartGame()
    {
        DeathScreenUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        DeathScreenUI.SetActive(false);
        SceneManager.LoadScene("Main Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
