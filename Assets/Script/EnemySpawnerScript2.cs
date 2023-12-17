using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript2 : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public float spawnRadius;
    [Header("SPAWN AMOUNTs")]
    public int spawnAmount;
    public float spawnAmount_increaseInterval;
    private float spawnAmount_increaseTimer;
    [Header("SPAWN SPEEDs")]
    public float spawnInterval_start;
    public float spawnInterval_decreaseRate;
    private float spawnTimer;
    #region SINGLETON
    public static EnemySpawnerScript2 me;
    private void Awake()
    {
        me = this;
    }
    #endregion
    private void Update()
    {


        if (spawnTimer > 0) // spawn cd
        {
            spawnTimer -= Time.deltaTime;
        }
        else
        {
            spawnTimer = spawnInterval_start;
            for (int i = 0; i < spawnAmount; i++) // when CDed, spawn one enemy
            {
                SpawnOne();
            }
        }
        DecreaseSpawnInterval();
        IncreaseSpawnAmount();
    }
    private void SpawnOne()
    {
        float enemyInsRandom_x = Random.Range(-spawnRadius, spawnRadius);
        float enemyInsRandom_z = Mathf.Sqrt(spawnRadius * spawnRadius - enemyInsRandom_x * enemyInsRandom_x);
        int randomDir = 1;
        if (Random.Range(-1.1f, 1.1f) < 0)
        {
            randomDir = -randomDir;
        }
        Vector3 enemyInsRandomPos = C_Player_Script.me.transform.position + new Vector3(enemyInsRandom_x, 0f, enemyInsRandom_z * randomDir);
        Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], enemyInsRandomPos, Quaternion.Euler(45f, 0f, 0f));
        //GameObject enemySpawned = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)]);
        //Vector3 spawnPos = RandomPointOnUnitCircle(spawnRadius);
        //spawnPos += C_Player_Script.me.transform.position;
        //enemySpawned.transform.position = enemyInsRandomPos;
    }
    public static Vector3 RandomPointOnUnitCircle(float radius)
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        float x = Mathf.Sin(angle) * radius;
        float y = Mathf.Cos(angle) * radius;

        return new Vector3(x, y, 0);
    }
    private void DecreaseSpawnInterval()
    {
        if (spawnInterval_start > 1)
        {
            spawnInterval_start -= spawnInterval_decreaseRate * Time.deltaTime;
        }
    }
    private void IncreaseSpawnAmount()
    {
        if (spawnAmount_increaseTimer > 0)
        {
            spawnAmount_increaseTimer -= Time.deltaTime;
        }
        else
        {
            spawnAmount_increaseTimer = spawnAmount_increaseInterval;
            spawnAmount++;
        }
    }
}
