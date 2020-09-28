using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EasterEgg : MonoBehaviour
{
    void Start()
    {

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
