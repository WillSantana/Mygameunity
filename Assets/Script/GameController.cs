using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //biblioteca de texto 
using UnityEngine.SceneManagement; //biblioteca de restart 

public class GameController : MonoBehaviour
{
    public int totalScore;
    public Text scoreText;
    public GameObject gameOver;

    public static GameController instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        scoreText.text = totalScore.ToString();
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
        
    }

    public void RestartGame(string lvlName)
    {
        SceneManager.LoadScene(lvlName);
    }
}
