using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;


public class MainMenuScript : MonoBehaviour
{

    public GameObject mainUI;
    public GameObject optionsUI;
    public GameObject levelsUI;

    public void Start()
    {
        Time.timeScale = 1;
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
        EditorApplication.isPlaying = false;
    }

}