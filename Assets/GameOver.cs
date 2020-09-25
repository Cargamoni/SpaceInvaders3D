using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    public Text scoreText;
    public Text bestScoreText;
    void Start()
    {
        //GameObject.Find("Canvas/PlayerScore").GetComponent<Text>().text = LevelCreator.score.ToString();

        int curScore = LevelCreator.score;
        scoreText.text = curScore.ToString();
        int prevBestScore = PlayerPrefs.GetInt("BestScore", 0);

        if (prevBestScore < curScore)
        {
            PlayerPrefs.SetInt("BestScore", curScore);
            prevBestScore = curScore;
        }
            
        bestScoreText.text = prevBestScore.ToString();

    }

    public void PlayAgain()
    {
        LevelCreator.score = 0;
        SceneManager.LoadScene("PlayGame");
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
