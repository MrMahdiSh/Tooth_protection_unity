using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public void SpawnEnemy(GameObject enemyPrefab)
    {
        GameObject spawnedEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        spawnedEnemy.transform.parent = transform;
    }
}
