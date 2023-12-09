using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V_HookOver_Brk_Dmg : EffectsCreater
{
    public DamageTag BasicSlashDamageTag;
    public bool ifinsBrkBox = true;

    //public Effect_CPU CPU_hooker;

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
        throw new System.NotImplementedException();
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
        EffectsPostOfficeScript.me.PO_DamageBox = new List<EffectsPostOfficeScript.damageletters>();
        BasicSlashDamageTag = DamageTag.Break;
        AttackDamege();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
