using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public GameObject deathMenuUI;
    public GameObject pauseMenuUI;
    public GameObject victoryMenuUI;
    public TextMeshProUGUI timerText;
    public float timer = 0;

    public void Start()
    {
        deathMenuUI = transform.Find("DeathScreen").gameObject;
        pauseMenuUI = transform.Find("PauseScreen").gameObject;
        victoryMenuUI = transform.Find("VictoryScreen").gameObject;
        timerText = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
       if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
        timer += Time.deltaTime;
        timerText.text = timer.ToString("F2")+"s";
        if (pauseMenuUI.activeSelf || victoryMenuUI.activeSelf){
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
        if(pauseMenuUI.activeSelf || deathMenuUI.activeSelf || victoryMenuUI.activeSelf)
        {
            pauseMenuUI.SetActive(false);
        }
        else
        {
            pauseMenuUI.SetActive(true);
        }
    }

    public void Deathscreen()
    {
        deathMenuUI.SetActive(true);
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
