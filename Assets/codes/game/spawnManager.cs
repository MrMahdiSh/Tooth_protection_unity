using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    public spawn[] spawns; // Spawner locations
    public GameObject[] enemyPrefabs;
    public GameObject bossPrefab;
    public float initialSpawnDelay = 1f;
    public float waveInterval = 10f;
    public float spanwInterval = 3f;
    public int maxWaves = 5;

    public int currentWave = 0;
    public bool spawn = false;
    public float counter;

    void Start()
    {
        Invoke("startSpawn", initialSpawnDelay);
    }

    void Update()
    {
        if (spawn)
        {
            counter += Time.deltaTime;
        }

        if (counter >= waveInterval && currentWave < maxWaves)
        {
            waveInterval += waveInterval;
            currentWave++;
        }

        if (counter >= spanwInterval)
        {
            spanwInterval += spanwInterval;
            SpawnEnemy();
        }
    }

    void startSpawn()
    {
        spawn = true;
    }

    void SpawnEnemy()
    {
        int randomSpawnerIndex = Random.Range(0, spawns.Length);
        spawn spawner = spawns[randomSpawnerIndex];
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        spawner.SpawnEnemy(enemyPrefab);
    }

    public void SpawnBoss()
    {
        int randomSpawnerIndex = Random.Range(0, spawns.Length);
        spawn spawner = spawns[randomSpawnerIndex];
        spawner.SpawnEnemy(bossPrefab);
    }

    public void bossSpawnHelper(float wait)
    {
        Invoke("SpawnBoss", wait);
    }


}
