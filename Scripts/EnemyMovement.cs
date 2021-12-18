using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    public float startSpeed;
    public float moveSpeed;
    public float startHealth;
    private float health = 0;
    public bool ignoreHit = false;

    public int netWorth;

    public GameObject deathEffect;

    public Image healthBar;
    public Material ignoredDamage;

    void Start()
    {
        moveSpeed = startSpeed;
        health = startHealth;
    }
    public void TakeDamage(float amount)
    {
        if (!ignoreHit)
        {
            health -= amount;
            healthBar.fillAmount = health / startHealth;
            if (health <= 0)
            {
                Die();
            }
        }
        else
        {
            this.gameObject.GetComponent<Renderer>().material = ignoredDamage;
            ignoreHit = false;
        }
    }
    void Die()
    {
        PlayerStats.Money += netWorth;
        netWorth = 0;
        //cast effect as gameobject so it can be destroyed
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        WaveSpawner.enemiesAlive--;
        //destoys this object
        Destroy(gameObject);
    }

    public void Slow(float amount)
    {
        moveSpeed = startSpeed * (1f - amount/100);
    }

}
