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
        SpawnEnemies();
        InvokeRepeating("SpawnEnemies", spawnInterval, spawnInterval);
    }

    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.U))
        // {
        //     spawn = !spawn;
        //     Debug.Log(spawn);
        // }

        // if (Input.GetKeyDown(KeyCode.I))
        // {
        //     int randomSpawner = Random.Range(0, spawnPoints.Length);

        //     spawn spawner = spawnPoints[randomSpawner];

        //     GameObject enemyPrefab = enemyPrefabs[0];

        //     spawner.SpawnEnemy(enemyPrefab);

        // }
    }

    void SpawnEnemies()
    {
        if (spawn)
        {
            int randomSpawner = Random.Range(0, spawnPoints.Length);

            spawn spawner = spawnPoints[randomSpawner];

            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            spawner.SpawnEnemy(enemyPrefab);
        }
    }
}
