using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_EnemySpawner_Script : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Enemy_SpawnBasic")]//用于控制基础的生成怪物参数。
    public GameObject Enemy_1;
    public float Enemy_SpawnRate = 3f;
    public float Enemy_SpawnTimer;
    public GameObject playerPos;

    void Start()
    {
        Enemy_SpawnTimer = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //在玩家周围15m的圈上生成基础怪物
        if (Enemy_SpawnTimer> Enemy_SpawnRate)
        {
            float enemyInsRandom_x = Random.Range(-10.1f, 10.1f);
            float enemyInsRandom_z = Mathf.Sqrt(10.1f*10.1f-enemyInsRandom_x* enemyInsRandom_x);
            int randomDir = 1;
            if (Random.Range(-1.1f, 1.1f) < 0)
            {
                randomDir = -randomDir;
            }
            Vector3 enemyInsRandomPos = playerPos.transform.position+new Vector3(enemyInsRandom_x,0f, enemyInsRandom_z* randomDir);
            Instantiate(Enemy_1, enemyInsRandomPos, Quaternion.Euler(45f, 0f, 0f));
            Enemy_SpawnTimer = 0;
        }
        else
        {
            Enemy_SpawnTimer += Time.deltaTime;
        }
    }
}
