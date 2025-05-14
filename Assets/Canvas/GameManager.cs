using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject DeathScreenUI;
    public GameObject PauseMenuUI;
    public GameObject victoryMenuUI;
    public float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;
        print(timer);
        if (PauseMenuUI.activeSelf || victoryMenuUI.activeSelf){
            Time.timeScale = 0f;
        }
        else{
            Time.timeScale = 1f;
        }
    }

    public void VictoryMenu()
    {
        victoryMenuUI.SetActive(true);
    }

    public void PauseMenu()
    {
        if(PauseMenuUI.activeSelf || DeathScreenUI.activeSelf || victoryMenuUI.activeSelf)
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level 1");
    }
    
    public void Level2()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void Level3()
    {
        SceneManager.LoadScene("Level 3");
    }

    public void Level4()
    {
        SceneManager.LoadScene("Level 4");
    }

    public void Level5()
    {
        SceneManager.LoadScene("Level 5");
    }   

}
