using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public static bool GameIsOver;

    public GameObject gameOverUI;
    public GameObject levelWonUI;
    public Text roundsText;
    [Tooltip("set 1 higher than current level")]
    public int levelToOpen = 0;

    void Start()
    {
        GameIsOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if ended stop running
        if (GameIsOver)
        {
            return;
        }
        if(PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }
    void EndGame()  //we lost the game/died
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        levelWonUI.SetActive(true);
        roundsText.text = PlayerStats.Rounds.ToString();
        if (levelToOpen !=0)
        {
            if (levelToOpen > PlayerPrefs.GetInt("levelReached", 1))
            {
                PlayerPrefs.SetInt("levelReached", levelToOpen);
            }
        }
    }


}
