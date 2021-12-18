using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public GameMaster gameMaster;
    public static int enemiesAlive;

    public Wave[] waves;
    public Transform spawnPoint;

    public float timeBetweenWaves;
    public float countdown;
    //public float waitTime;

    public Text waveCountdowntext;

    private int waveNumber = 0;

    public void Start()
    {
        enemiesAlive = 0;
    }

    void Update()
    {
        if (enemiesAlive > 0)
        {
            return;
        }
        if (enemiesAlive == 0 && waveNumber == waves.Length)
        {
            gameMaster.WinLevel();
            this.enabled = false;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(Spawnwave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdowntext.text = string.Format("{0:00.00}", countdown);

    }

    IEnumerator Spawnwave()
    {
        PlayerStats.Rounds++;

        Wave wave = waves[waveNumber];

        enemiesAlive = wave.enemyAmount + wave.enemy2Amount;
        Debug.Log("enemies alive" + enemiesAlive);
        if (waveNumber > waves.Length)
        {
            StopCoroutine(Spawnwave());
        }
                //randomises the enemies
        if (wave.randomizeWave)
        {
            for (int i = 0; i < wave.enemyAmount; i++)
            {
                int k = Random.Range(0, 10);
                if (k < 7)
                {
                    if (wave.scenicRoute)
                    {
                        wave.enemyPrefab.GetComponent<EnemyMove>().takeReroute = true;
                    }
                    if (wave.RNGRoute)
                    {
                        int blessRNG = Random.Range(0, 15);
                        if (blessRNG < 8)
                        {
                            wave.enemyPrefab.GetComponent<EnemyMove>().takeReroute = true;
                        }
                    }
                    SpawnEnemy(wave.enemyPrefab);
                    wave.enemyPrefab.GetComponent<EnemyMove>().takeReroute = false;
                    yield return new WaitForSeconds(1f / wave.rate);
                }
                else
                {
                    if (wave.scenicRoute)
                    {
                        wave.enemyPrefab2.GetComponent<EnemyMove>().takeReroute = true;
                    }
                    if (wave.RNGRoute)
                    {
                        int blessRNG = Random.Range(0, 15);
                        if (blessRNG < 8)
                        {
                            wave.enemyPrefab2.GetComponent<EnemyMove>().takeReroute = true;
                        }
                    }
                    SpawnEnemy(wave.enemyPrefab2);
                    wave.enemyPrefab2.GetComponent<EnemyMove>().takeReroute = false;
                    yield return new WaitForSeconds(1f / wave.rate);
                }
            }
        }
        else
        {
            for (int i = 0; i < wave.enemyAmount; i++)
            {
                wave.enemyPrefab.GetComponent<EnemyMove>().takeReroute = false;
                if (wave.RNGRoute)
                {
                    int blessRNG = Random.Range(0,15);
                    if (blessRNG < 8)
                    {
                        wave.enemyPrefab.GetComponent<EnemyMove>().takeReroute = true;
                    }
                }
                if (wave.scenicRoute)
                {
                    wave.enemyPrefab.GetComponent<EnemyMove>().takeReroute = true;
                }
                SpawnEnemy(wave.enemyPrefab);
                yield return new WaitForSeconds(1f / wave.rate);
            }
            for (int i = 0; i < wave.enemy2Amount; i++)
            {
                wave.enemyPrefab2.GetComponent<EnemyMove>().takeReroute = false;
                if (wave.RNGRoute)
                {
                    int blessRNG = Random.Range(0, 15);
                    if (blessRNG < 5)
                    {
                        wave.enemyPrefab2.GetComponent<EnemyMove>().takeReroute = true;
                    }
                }
                if (wave.scenicRoute)
                {
                    wave.enemyPrefab2.GetComponent<EnemyMove>().takeReroute = true;
                }
                SpawnEnemy(wave.enemyPrefab2);
                yield return new WaitForSeconds(1f / wave.rate);
            }
        }

        waveNumber++;

    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

}
