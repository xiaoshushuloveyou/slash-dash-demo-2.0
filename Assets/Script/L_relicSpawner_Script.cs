using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_relicSpawner_Script : MonoBehaviour
{
    public GameObject relicM;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 relicInsPos = new Vector3(0f,1f,8f);
        GameObject healthPoint_n = Instantiate(relicM, relicInsPos, Quaternion.Euler(0f, 0f, 40f));
        for (int i = 0; i < 25; i++)
        {
            Instantiate(relicM, new Vector3(50f - i * 4f, 1, -50f), Quaternion.Euler(0f, 0f, 0f));
            Instantiate(relicM, new Vector3(50f, 1, 50f-i*4f), Quaternion.Euler(0f, 0f, 0f));
            Instantiate(relicM, new Vector3(-50f, 1, -50f+i * 4f), Quaternion.Euler(0f, 0f, 0f));
            Instantiate(relicM, new Vector3(-50f+i * 4f, 1, 50f), Quaternion.Euler(0f, 0f, 0f));
        }
        
        for (int i = 0; i < 8; i++)
        {
            int randomDir1 = 1;
            int randomDir2 = 1;
            float randomX = Random.Range(10.1f, 40.1f);
            float randomY = Random.Range(10.1f, 40.1f);
            if (Random.Range(-1.1f,1.1f)<0)
            {
                randomDir1 = -randomDir1;
            }
            if (Random.Range(-1.1f, 1.1f) < 0)
            {
                randomDir2 = -randomDir2;
            }
            Vector3 relicInsPosiiton = new Vector3(randomX * randomDir1,1f, randomY * randomDir2);
            Instantiate(relicM, relicInsPosiiton, Quaternion.Euler(0f, 0f, Random.Range(-40.1f, 40.1f)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
