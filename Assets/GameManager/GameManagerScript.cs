using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public GameObject victoryUI;
    public GameObject gameMenuUI;
    public GameObject gameOverUI;
    public GameObject levelUpUI;
    public GameObject gamemenuScreenUI;
    public GameObject optionsScreenUI;
    public PlayerController speed;
    public SwordAttack damage;
    public PlayerHealth shield;
    public PlayerManager playerList;

    void Start(){
        playerList = GameObject.Find("PlayerSpawner").GetComponent<PlayerManager>();
    }
    private void Update()
    {
        if (levelUpUI.activeSelf || gameMenuUI.activeSelf || gameOverUI.activeSelf || victoryUI.activeSelf){
            Time.timeScale = 0;
        }else{
            Time.timeScale = 1;
        }
        if(gameMenuUI.activeSelf == false){
            optionsScreenUI.SetActive(false);
            gamemenuScreenUI.SetActive(true);
        }
    }
    //GameMenu
    public void GameMenu()
    {
        if(gameMenuUI.activeSelf || gameOverUI.activeSelf || levelUpUI.activeSelf || victoryUI.activeSelf){
            gameMenuUI.SetActive(false);
        }else{
            gameMenuUI.SetActive(true);
        }
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene("BasementMain");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    //Level Up Menu
    public void LevelUp(){
        levelUpUI.SetActive(true);
    }
    public void Speed(){
        foreach(GameObject player in playerList.gameObjects){
            player.GetComponent<PlayerController>().maxSpeed *= 1.05f;
        }
        levelUpUI.SetActive(false);
    }
    public void Damage(){
        foreach(GameObject player in playerList.gameObjects){
            player.GetComponentInChildren<SwordAttack>().swordDamage = player.GetComponentInChildren<SwordAttack>().swordDamage * 1.25f;
            player.GetComponentInChildren<LightningBoltSpawner>().boltDMG = player.GetComponentInChildren<LightningBoltSpawner>().boltDMG * 1.25f;
        }
        levelUpUI.SetActive(false);
    }
    public void Shield(){
        foreach(GameObject player in playerList.gameObjects){
            player.GetComponent<PlayerHealth>()._health += 1;
        }
        levelUpUI.SetActive(false);
    }
}