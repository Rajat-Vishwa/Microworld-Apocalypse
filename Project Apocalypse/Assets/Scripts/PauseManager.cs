using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseManager : MonoBehaviour
{   
    public GameObject pauseMenu, gameOverMenu;
    public GameManager gameManager;
    
    public bool isPaused = false;
    void Update()
    {
        if(Input.GetKeyDown("escape")){
            if(!isPaused) Pause();
            else Resume();
        }
        gameManager = gameObject.GetComponent<GameManager>();
    }

    public void Pause(){
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        isPaused = true;
    }
    public void Resume(){
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    public void Restart(){
        Resume();
        SceneManager.LoadScene("MainScene");    
    }
    public void MainMenu(){
        Resume();
        SceneManager.LoadScene("MainMenu");
    }

    public void GameOver(){
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);

        int highScore = PlayerPrefs.GetInt("HighScore");

        if(gameManager.CurrentScore > highScore){
            PlayerPrefs.SetInt("HighScore", gameManager.CurrentScore);
        }

        TMP_Text highScoreText = gameOverMenu.GetComponentInChildren<TMP_Text>();
        highScoreText.text = "HighScore : " + PlayerPrefs.GetInt("HighScore").ToString();
    }  

}
