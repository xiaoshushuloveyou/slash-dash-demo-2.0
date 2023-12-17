using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KardManagerScript : MonoBehaviour
{
    public List<GameObject> Kardlist;
    public List<GameObject> KardIDpool;
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
        Kardlist = new List<GameObject>();
        for (int i = 0; i < GameObject.Find("Gamedata").GetComponent<GameData>().Deck.Count; i++)
        {
            if (GameObject.Find("Gamedata").GetComponent<GameData>().Deck[i] >=0)
            {
                Kardlist.Add(KardIDpool[GameObject.Find("Gamedata").GetComponent<GameData>().Deck[i]]);
            }
        }

        if (Kardlist.Count>0)
        {
            //Instantiate(Kardlist[nowCardNum]);
            GameObject cardnew = Instantiate(Kardlist[nowCardNum]);
            cardnew.transform.SetParent(transform);
            cardnew.transform.position = transform.position;
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
            //print(EffectsPostOfficeScript.me.PO_senderInfo.SenderCardState + "__________" + EffectsPostOfficeScript.me.PO_senderInfo.SenderHolderState);
            EffectsPostOfficeScript.me.PO_senderInfo = new EffectsPostOfficeScript.senderState("StateIdle", "CardReleasedTiming");
            GameObject cardnew =  Instantiate(Kardlist[nowCardNum]);
            cardnew.transform.SetParent(transform);
            cardnew.transform.position = transform.position;
            //print(EffectsPostOfficeScript.me.PO_senderInfo.SenderCardState + "__________" + EffectsPostOfficeScript.me.PO_senderInfo.SenderHolderState);
            nextCard = false;
        }

    }
}
