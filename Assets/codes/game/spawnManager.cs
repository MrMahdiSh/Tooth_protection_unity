using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public spawn[] spawnPoints;
    public GameObject bossPrefab;
    public spawn bossSpawnPoint;

    public float spawnInterval = 6f;
    public bool spawn;
    public float intervalDecreaseRate = 0.1f;
    public float minimumSpawnInterval = 1f;
    public float decreaseInterval = 30f;

    void Start()
    {
        startSpawn();
    }

    public void startSpawn()
    {
        SpawnEnemies();
        InvokeRepeating("SpawnEnemies", spawnInterval, spawnInterval);
        StartCoroutine(DecreaseSpawnInterval());
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

    IEnumerator DecreaseSpawnInterval()
    {
        while (spawnInterval > minimumSpawnInterval)
        {
            yield return new WaitForSeconds(decreaseInterval); // Decrease interval every 'decreaseInterval' seconds
            spawnInterval = Mathf.Max(spawnInterval - intervalDecreaseRate, minimumSpawnInterval);
            CancelInvoke("SpawnEnemies");
            InvokeRepeating("SpawnEnemies", spawnInterval, spawnInterval);
        }
    }

    public void bossSpawnHelper(float min)
    {
        Invoke("spawnBoss", min * 60);
    }

    void spawnBoss()
    {
        if (spawn)
        {
            bossSpawnPoint.SpawnEnemy(bossPrefab);
        }
    }

}
