using UnityEngine;

[System.Serializable]
public class Wave
{
    [Tooltip("enemy to spawn")]
    public GameObject enemyPrefab;
    [Tooltip("enemies per wave")]
    public int enemyAmount;
    [Tooltip("second wave bois")]
    public GameObject enemyPrefab2;
    [Tooltip("second wave amount")]
    public int enemy2Amount = 0;
    [Tooltip("means per second")]
    public float rate;
    [Tooltip("spawns random enemies based on the prefabs used")]
    public bool randomizeWave;
    [Tooltip("enables the rng scenic route")]
    public bool RNGRoute;
    [Tooltip("all enemies in this wave goes the 2nd route")]
    public bool scenicRoute;

}
