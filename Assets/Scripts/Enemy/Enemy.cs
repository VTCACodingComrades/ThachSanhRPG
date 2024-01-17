using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public string enemyName;
    public GameObject deathEffect;


    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DeathEffect();
            gameObject.SetActive(false);
        }
    }

    private void DeathEffect()
    {
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
    }
}
