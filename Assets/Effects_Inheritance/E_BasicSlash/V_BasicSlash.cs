using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V_BasicSlash : EffectsCreater
{
    private Vector3 velocity = Vector3.zero;
    private Vector3 slashTarget;
    public float slashSmooth=1f;
    public HolderState BasicSlashHolderState;
    public CardState BasicSlashCardState;
    public DamageTag BasicSlashDamageTag;
    private bool ifMouse0Up = false;

    public List<GameObject> enemiesEffected;
    

    #region SINGLETON
    public static V_BasicSlash me;
    private void Awake()
    {
        me = this;
    }
    #endregion
    public override void AttackDamege()
    {
        EffectMessenger(BasicSlashDamageTag.ToString(),2);
    }

    public override void EffectMessenger(string Dtype, int Dnum)
    {
        EffectsPostOfficeScript.me.PO_DamageBox.Add(new EffectsPostOfficeScript.damageletters(Dtype,Dnum));
    }

    public override void cardInteraction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BasicSlashCardState = CardState.CardReleasedTiming;
            prefabInstantiater();
            slashTarget = effectHolder.transform.position;
        }
        if (Input.GetMouseButtonUp(0))
        {
            
            BasicSlashCardState = CardState.CardActivating;
            BasicSlashHolderState = HolderState.Moving;
            slashTarget = V_BasicSlash_PathCollision.me.childpathEndPointPo;
            ifMouse0Up = true;
           
        }
        if (Vector3.SqrMagnitude(effectHolder.transform.position - slashTarget) <= 0.005f&& ifMouse0Up)
        {
            BasicSlashHolderState = HolderState.StateIdle;
            BasicSlashCardState = CardState.CardEndTiming;
            ifMouse0Up = false;
        }
        effectHolder.transform.position = Vector3.SmoothDamp(effectHolder.transform.position, slashTarget, ref velocity, slashSmooth / 3.7f);
    }

    public override void C_NewcolliderInstantiater()
    {
        
    }

    public override void prefabInstantiater()
    {
        GameObject slashpash = Instantiate(prefabIns);
    }

    // Start is called before the first frame update
    void Start()
    {
        effectHolder = GameObject.Find("C_Player");
        EffectsPostOfficeScript.me.PO_DamageBox = new List<EffectsPostOfficeScript.damageletters>();
        BasicSlashDamageTag = DamageTag.Damage;
        AttackDamege();
        slashTarget = effectHolder.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        cardInteraction();
        stateAnimator();
        PO_senderInfoSyn();
    }

    private void PO_senderInfoSyn()
    {
        
        EffectsPostOfficeScript.me.PO_senderInfo = new EffectsPostOfficeScript.senderState(BasicSlashHolderState.ToString(), BasicSlashCardState.ToString());
        //print("StateSyn" + EffectsPostOfficeScript.me.PO_senderInfo);
    }
    private void stateAnimator()
    {
        if (BasicSlashHolderState == HolderState.Moving)
        {
            C_Player_Script.me.SPUM_Ani.PlayAnimation(4);
        }
        else
        {
            C_Player_Script.me.SPUM_Ani._anim.ForceStateNormalizedTime(1);
            C_Player_Script.me.SPUM_Ani.PlayAnimation(0);
        }
    }
}
