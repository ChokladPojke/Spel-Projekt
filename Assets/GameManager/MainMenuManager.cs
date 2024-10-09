using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    //MainMenu
    public void QuitGame()
    {
        Application.Quit();
    }
    //ModesScript
    public void OnEasy(){
        PlayerPrefs.SetFloat("Difficulty",1);
        SceneManager.LoadScene("BasementMain");
    }
    public void OnMedium(){
        PlayerPrefs.SetFloat("Difficulty",2);
        SceneManager.LoadScene("BasementMain");
    }
    public void OnHard(){
        PlayerPrefs.SetFloat("Difficulty",3);
        SceneManager.LoadScene("BasementMain");
    }
}
