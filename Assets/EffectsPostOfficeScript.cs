using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EffectsPostOfficeScript : MonoBehaviour
{
    //OfficeMailBox
    public senderState PO_senderInfo;
    public List<damageletters>  PO_DamageBox;

    private void Update()
    {
        //if (Input.GetMouseButtonUp(0))
        //{
        //    for (int i = 0; i < PO_DamageBox.Count; i++)
        //    {
        //        print(PO_DamageBox[i].EffectType);
        //        print(PO_DamageBox[i].Damage);
        //        print("________________________________");
        //    }
        //}

        if (PO_senderInfo.SenderCardState == "CardEndTiming")
        {
            PO_DamageBox = new List<damageletters>();
        }


    }




    private void Start()
    {
        PO_DamageBox = new List<damageletters>();
    }
    [Serializable]
    public class senderState
    {
        private string senderHolderState;
        private string senderCardState;
        public string SenderHolderState
        {
            get { return senderHolderState; }
        }
        public string SenderCardState
        {
            get { return senderCardState; }
        }
        public senderState(string SenderHolderState, string SenderCardState)
        {
            this.senderHolderState = SenderHolderState;
            this.senderCardState = SenderCardState;
        }
    }
    [Serializable]
    public class damageletters
    {
        private string effectType;
        private int damage;
        public string EffectType
        {
            get { return effectType; }
        }
        public int Damage
        {
            get { return damage; }
        }
        public damageletters(string EffectType, int Damage)
        {
            this.effectType = EffectType;
            this.damage = Damage;
        }
    }

    #region SINGLETON
    public static EffectsPostOfficeScript me;
    private void Awake()
    {
        me = this;
    }
    #endregion

}
