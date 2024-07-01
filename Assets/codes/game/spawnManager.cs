using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array to hold all enemy prefabs

    public spawn[] spawnPoints; // Array to hold all spawn points

    public float spawnInterval = 6f; // Interval between spawns in seconds

    public bool spawn;

    public void startSpawn()
    {
        if (spawn)
        {
            SpawnEnemies();
            InvokeRepeating("SpawnEnemies", spawnInterval, spawnInterval);
        }
    }

    void SpawnEnemies()
    {
        int randomSpawner = Random.Range(0, spawnPoints.Length);

        spawn spawner = spawnPoints[randomSpawner];

        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        spawner.SpawnEnemy(enemyPrefab);
    }
}
