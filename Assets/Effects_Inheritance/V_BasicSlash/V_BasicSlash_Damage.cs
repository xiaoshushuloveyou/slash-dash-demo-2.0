using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V_BasicSlash_Damage : EffectsCreater
{
    public DamageTag BasicSlashDamageTag;
    public CardState BasicSlashCardState;
    public GameObject BasicSlash_DmgBox;
    public bool ifinsDmgBox = true;

    public override void AttackDamege()
    {
        EffectMessenger(BasicSlashDamageTag.ToString(),2);
    }

    public override void EffectMessenger(string Dtype, int Dnum)
    {
        EffectsPostOfficeScript.me.PO_DamageBox.Add(new EffectsPostOfficeScript.damageletters(Dtype,Dnum));
    }

    // Start is called before the first frame update
    void Start()
    {
        
        //EffectsPostOfficeScript.me.PO_DamageBox = new List<EffectsPostOfficeScript.damageletters>();
        BasicSlashDamageTag = DamageTag.Damage;
        AttackDamege();

        effectHolder = GameObject.Find("C_Player");

    }

    // Update is called once per frame
    void Update()
    {

        C_NewcolliderInstantiater();
    }

    public override void C_NewcolliderInstantiater()
    {
        if (ifinsDmgBox && EffectsPostOfficeScript.me.PO_senderInfo.SenderHolderState == "Moving")
        {
            //ins the damage box
            GameObject dmgbox = Instantiate(BasicSlash_DmgBox);
            dmgbox.transform.position = effectHolder.transform.position;
            dmgbox.transform.SetParent(effectHolder.transform, true);
            //dmgbox.transform.parent = effectHolder.transform;
            ifinsDmgBox = false;
            effectHolder.transform.GetComponent<SphereCollider>().enabled = false;
        }

        if (!ifinsDmgBox && EffectsPostOfficeScript.me.PO_senderInfo.SenderHolderState != "Moving")
        {
            ifinsDmgBox = true;            
            effectHolder.transform.GetComponent<SphereCollider>().enabled = true;
            BasicSlashCardState = CardState.CardEndTiming;
            PO_senderInfoSyn();
        }
    }
    private void PO_senderInfoSyn()
    {
        EffectsPostOfficeScript.me.PO_senderInfo = new EffectsPostOfficeScript.senderState(EffectsPostOfficeScript.me.PO_senderInfo.SenderHolderState, BasicSlashCardState.ToString());
    }
    public override void prefabInstantiater()
    {
        
    }
    public override void cardInteraction()
    {

    }
}
