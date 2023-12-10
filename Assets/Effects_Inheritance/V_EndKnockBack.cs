using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V_EndKnockBack : EffectsCreater
{

    public GameObject KnockBackBox;
    public bool ifinstheKBboxalready = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if cardendtiming, instantiate one knock back box
        if (EffectsPostOfficeScript.me.PO_senderInfo.SenderCardState == "CardEndTiming" && !ifinstheKBboxalready)
        {
            GameObject knockbackbox = Instantiate(KnockBackBox);
            print("YYYYYYYYYYYYYYYY");
            knockbackbox.transform.position = C_Player_Script.me.transform.position;
            ifinstheKBboxalready = true;
        }
    }


    public override void AttackDamege()
    {
        throw new System.NotImplementedException();
    }

    public override void cardInteraction()
    {
        throw new System.NotImplementedException();
    }

    public override void C_NewcolliderInstantiater()
    {
        throw new System.NotImplementedException();
    }

    public override void EffectMessenger(string Dtype, int Dnum)
    {
        throw new System.NotImplementedException();
    }

    public override void prefabInstantiater()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    
}
