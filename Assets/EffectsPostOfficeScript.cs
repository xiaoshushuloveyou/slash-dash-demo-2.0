using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EffectsPostOfficeScript : MonoBehaviour
{
    //OfficeMailBox
    public senderState PO_senderInfo;
    public List<damageletters>  PO_DamageBox;

    //private void Update()
    //{
    //    if (PO_senderInfo.SenderHolderState == "Moving")
    //    {
    //        print(PO_DamageBox[0].Damage);
    //    }

    //}




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
