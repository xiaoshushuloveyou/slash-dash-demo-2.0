using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V_HookOver_Brk_Inter : EffectsCreater
{

    public HolderState BasicSlashHolderState;
    public CardState BasicSlashCardState;
    private Vector3 velocity = Vector3.zero;
    private Vector3 slashTarget;
    public float slashSmooth = 1f;
    private bool ifMouse0Up = false;

    public GameObject hookerBox;

    public override void cardInteraction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BasicSlashCardState = CardState.CardReleasedTiming;
            BasicSlashHolderState = HolderState.StateIdle;
            PO_senderInfoSyn();

            prefabInstantiater();
            slashTarget = effectHolder.transform.position;
        }
        if (Input.GetMouseButtonUp(0))
        {

            BasicSlashCardState = CardState.CardActivating;
            BasicSlashHolderState = HolderState.StateIdle;
            PO_senderInfoSyn();
            slashTarget = V_BasicSlash_PathCollision.me.childpathEndPointPo;
            ifMouse0Up = true;

        }
        if (ifMouse0Up)
        {
            GameObject hookerbox = Instantiate(hookerBox);
            hookerbox.transform.position = C_Player_Script.me.transform.position;
            hookerBoxScript.me.hookTargetPo = slashTarget;
            ifMouse0Up = false;
        }
        
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
