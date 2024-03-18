using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRadius = 10f;
    public int currentWaveNumber = 1;
    public int totalWaveToSpawn = 5;

    // Start is called before the first frame update
    void Start()
    {
        SpawnWave();
    }

    public void SpawnWave()
    {
        for (int i = 0; i < currentWaveNumber; i++)
        {
            // Generate a random position within a circle
            Vector2 spawnPosition = Random.insideUnitCircle * spawnRadius;

            // Instantiate the enemy at the random position
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }

        // Increase the wave number for the next wave
        currentWaveNumber++;
    }

    // Update is called once per frame
    public void Update()
    {
        // If there are no enemies left, spawn the next wave
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && currentWaveNumber < totalWaveToSpawn)
        {
            SpawnWave();
        }
        else if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && currentWaveNumber >= totalWaveToSpawn)
        {
            Destroy(gameObject);
        }
    }
}
