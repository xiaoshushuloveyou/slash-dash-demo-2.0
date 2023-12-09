using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V_BreakSlash_Damage : EffectsCreater
{
    public DamageTag BasicSlashDamageTag;
    public CardState BasicSlashCardState;
    public GameObject BasicSlash_BrkBox;
    public bool ifinsBrkBox = true;
    public override void AttackDamege()
    {
        EffectMessenger(BasicSlashDamageTag.ToString(), 2);
    }

    public override void cardInteraction()
    {
        throw new System.NotImplementedException();
    }

    public override void C_NewcolliderInstantiater()
    {
        if (ifinsBrkBox && EffectsPostOfficeScript.me.PO_senderInfo.SenderHolderState == "Moving")
        {
            //ins the damage box
            GameObject dmgbox = Instantiate(BasicSlash_BrkBox);
            dmgbox.transform.position = effectHolder.transform.position;
            dmgbox.transform.SetParent(effectHolder.transform, true);
            //dmgbox.transform.parent = effectHolder.transform;
            ifinsBrkBox = false;
            effectHolder.transform.GetComponent<SphereCollider>().enabled = false;
        }

        if (!ifinsBrkBox && EffectsPostOfficeScript.me.PO_senderInfo.SenderHolderState != "Moving")
        {
            ifinsBrkBox = true;
            effectHolder.transform.GetComponent<SphereCollider>().enabled = true;
            if (BasicSlashCardState != CardState.CardEndTiming)
            {
                BasicSlashCardState = CardState.CardEndTiming;
            }
            
            PO_senderInfoSyn();
        }
    }

    public override void EffectMessenger(string Dtype, int Dnum)
    {
        EffectsPostOfficeScript.me.PO_DamageBox.Add(new EffectsPostOfficeScript.damageletters(Dtype, Dnum));
    }

    public override void prefabInstantiater()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        //EffectsPostOfficeScript.me.PO_DamageBox = new List<EffectsPostOfficeScript.damageletters>();
        BasicSlashDamageTag = DamageTag.Break;
        AttackDamege();

        effectHolder = GameObject.Find("C_Player");
    }

    // Update is called once per frame
    void Update()
    {
        C_NewcolliderInstantiater();
    }
    private void PO_senderInfoSyn()
    {
        EffectsPostOfficeScript.me.PO_senderInfo = new EffectsPostOfficeScript.senderState(EffectsPostOfficeScript.me.PO_senderInfo.SenderHolderState, BasicSlashCardState.ToString());
    }
}
