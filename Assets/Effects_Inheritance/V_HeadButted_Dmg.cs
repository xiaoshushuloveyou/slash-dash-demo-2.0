using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V_HeadButted_Dmg : EffectsCreater
{
    public DamageTag BasicSlashDamageTag;
    public CardState BasicSlashCardState;
    public GameObject HeadButtedBox;
    public bool ifinsHeadButtedBox = true;
    public Effect_CPU CPUscript;
    public override void AttackDamege()
    {
        throw new System.NotImplementedException();
    }
    public override void EffectMessenger(string Dtype, int Dnum)
    {
        
    }

    void Start()
    {
        effectHolder = GameObject.Find("C_Player");
    }

    // Update is called once per frame
    void Update()
    {
        C_NewcolliderInstantiater();
    }

    public override void C_NewcolliderInstantiater()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //instantiate the headbuttedbox show the sprite.
            GameObject headbuttedbox = Instantiate(HeadButtedBox);
            //headbuttedbox.transform.SetParent(effectHolder.transform, true);
        }

        if (ifinsHeadButtedBox && EffectsPostOfficeScript.me.PO_senderInfo.SenderHolderState == "Moving")
        {
            //ins the damage box
            //GameObject dmgbox = Instantiate(BasicSlash_HeadButtedBox);
            //dmgbox.transform.position = effectHolder.transform.position;
            //dmgbox.transform.SetParent(effectHolder.transform, true);
            //dmgbox.transform.parent = effectHolder.transform;
            ifinsHeadButtedBox = false;
            effectHolder.transform.GetComponent<SphereCollider>().enabled = false;
        }

        if (!ifinsHeadButtedBox && EffectsPostOfficeScript.me.PO_senderInfo.SenderHolderState != "Moving")
        {
            ifinsHeadButtedBox = true;
            effectHolder.transform.GetComponent<SphereCollider>().enabled = true;
            if (BasicSlashCardState != CardState.CardEndTiming)
            {
                BasicSlashCardState = CardState.CardEndTiming;
            }
            PO_senderInfoSyn();
        }
    }

    private void PO_senderInfoSyn()
    {
        EffectsPostOfficeScript.me.PO_senderInfo = new EffectsPostOfficeScript.senderState(EffectsPostOfficeScript.me.PO_senderInfo.SenderHolderState, BasicSlashCardState.ToString());
    }

    public override void prefabInstantiater()
    {
        throw new System.NotImplementedException();
    }

    public override void cardInteraction()
    {
        throw new System.NotImplementedException();
    }
    // Start is called before the first frame update

}
