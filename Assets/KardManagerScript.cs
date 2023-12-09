using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KardManagerScript : MonoBehaviour
{
    public List<GameObject> Kardlist;
    public List<GameObject> HandKard;
    public bool nextCard = false;
    public int nowCardNum = 0;

    #region SINGLETON
    public static KardManagerScript me;
    private void Awake()
    {
        me = this;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        if (Kardlist.Count>0)
        {
            Instantiate(Kardlist[nowCardNum]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (nowCardNum==Kardlist.Count)
        {
            nowCardNum = 0;
            //nextCard = false;
        }
        if (nextCard)
        {
            Instantiate(Kardlist[nowCardNum]);
            nextCard = false;
        }

    }
}
