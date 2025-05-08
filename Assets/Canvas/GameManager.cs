using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject DeathScreen;

    private void Update()
    {
        // if(gameMenuUI.activeSelf == false){
        //     optionsScreenUI.SetActive(false);
        //     gamemenuScreenUI.SetActive(true);
        // }
    }
    //GameMenu
    // public void GameMenu()
    // {
    //     if(gameMenuUI.activeSelf || DeathScreen.activeSelf || levelUpUI.activeSelf || victoryUI.activeSelf){
    //         gameMenuUI.SetActive(false);
    //     }else{
    //         gameMenuUI.SetActive(true);
    //     }
    // }

    public void Deathscreen()
    {
        DeathScreen.SetActive(true);
    }
    
    public void RestartGame()
    {
        DeathScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        DeathScreen.SetActive(false);
        SceneManager.LoadScene("Main Menu");
    }


    public void QuitGame()
    {
        Application.Quit();
    }

}
