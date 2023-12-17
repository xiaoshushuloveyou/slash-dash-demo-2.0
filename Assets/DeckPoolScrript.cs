using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckPoolScrript : MonoBehaviour
{
    public List<GameObject> Poollist;
    public float insdir = 0;
    public float insdir2 = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Poollist.Count; i++)
        {
            GameObject insCarpool = Instantiate(Poollist[i]);
            insCarpool.transform.SetParent(transform);
            //Debug.Log("Instantiate");
            if (i<=Mathf.Round(Poollist.Count/2))
            {
                insCarpool.transform.position = transform.position + new Vector3(0, -insdir, 0f);
                insdir += 100f;
            }
            else
            {
                insCarpool.transform.position = transform.position + new Vector3(100f, -insdir2, 0f);
                insdir2 += 100f;
            }
         
            
        }
    }


}
