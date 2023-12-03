using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kard_BasicSlashScript : MonoBehaviour
{

    #region SINGLETON
    public static Kard_BasicSlashScript me;
    private void Awake()
    {
        me = this;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        EffectsPostOfficeScript.me.PO_senderInfo = new EffectsPostOfficeScript.senderState("StateIdle", "CardReleasedTiming");
    }

    // Update is called once per frame
    void Update()
    {

        if (EffectsPostOfficeScript.me.PO_senderInfo.SenderCardState == "CardEndTiming" )
        {
            
            Destroy(gameObject);
            KardManagerScript.me.nowCardNum += 1;
            KardManagerScript.me.nextCard = true;
            //
        }

    }
}
