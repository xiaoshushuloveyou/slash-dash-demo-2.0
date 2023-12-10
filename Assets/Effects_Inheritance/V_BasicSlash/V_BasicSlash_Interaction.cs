using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V_BasicSlash_Interaction : EffectsCreater
{
    public HolderState BasicSlashHolderState;
    public CardState BasicSlashCardState;
    private Vector3 velocity = Vector3.zero;
    private Vector3 slashTarget;
    public float slashSmooth = 1f;
    private bool ifMouse0Up = false;


    public override void cardInteraction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BasicSlashCardState = CardState.CardReleasedTiming;
            PO_senderInfoSyn();
            prefabInstantiater();
            slashTarget = effectHolder.transform.position;
        }
        if (Input.GetMouseButtonUp(0))
        {

            BasicSlashCardState = CardState.CardActivating;
            BasicSlashHolderState = HolderState.Moving;
            PO_senderInfoSyn();
            slashTarget = V_BasicSlash_PathCollision.me.childpathEndPointPo;
            ifMouse0Up = true;

        }
        if (Vector3.SqrMagnitude(effectHolder.transform.position - slashTarget) <= 0.005f && ifMouse0Up)
        {
            BasicSlashHolderState = HolderState.StateIdle;
            if (BasicSlashCardState != CardState.CardEndTiming)
            {
                BasicSlashCardState = CardState.CardEndTiming;
            }
            PO_senderInfoSyn();
            ifMouse0Up = false;
        }
        effectHolder.transform.position = Vector3.SmoothDamp(effectHolder.transform.position, slashTarget, ref velocity, slashSmooth / 3.7f);
    }


    // Start is called before the first frame update
    void Start()
    {
        effectHolder = GameObject.Find("C_Player");
        slashTarget = effectHolder.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        stateAnimator();
 
        cardInteraction();
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
    private void PO_senderInfoSyn()
    {
        EffectsPostOfficeScript.me.PO_senderInfo = new EffectsPostOfficeScript.senderState(BasicSlashHolderState.ToString(), BasicSlashCardState.ToString());
    }
    public override void AttackDamege()
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
        GameObject slashpash = Instantiate(prefabIns);
    }
}
