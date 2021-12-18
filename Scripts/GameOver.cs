using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text roundsText;
    private int rounds = 0;

    void OnEnable()
    {
        rounds = PlayerStats.Rounds - 1;
        roundsText.text = rounds.ToString();
        StartCoroutine(SlowTime());
    }
    
    public IEnumerator SlowTime()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
