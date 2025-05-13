using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public GameObject mainUI;
    public GameObject optionsUI;
    public GameObject levelsUI;
    public int selectedLevel;
    
    public void LevelSelect()
    {
        SceneManager.LoadScene("Level "+selectedLevel);
    }

    public void LevelsMenu()
    {
        mainUI.SetActive(false);
        levelsUI.SetActive(true);
    }

    public void OptionsMenu()
    {
        mainUI.SetActive(false);
        optionsUI.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}