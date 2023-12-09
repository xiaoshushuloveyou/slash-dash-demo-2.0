using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EffectsCreater :MonoBehaviour
{
    [Header("Basic Info")]
    public string effectName;
    public int effectID;
    public GameObject effectHolder;
    public GameObject effectTarget;
    public GameObject prefabIns;

    //public List<effectletters> effectmessletters = new List<effectletters>();
    //public class effectletters
    //{
    //    private string effectType;
    //    private int damage;
    //    public string EffectType
    //    {
    //        get { return effectType; }
    //    }
    //    public int Damage
    //    {
    //        get { return damage; }
    //    }
    //    public effectletters(string EffectType,int Damage)
    //    {
    //        this.effectType = EffectType;
    //        this.damage = Damage;
    //    }
    //}

    //effect act on info
    public enum HolderState
    {
        StateIdle,
        Moving,
    };
    public enum CardState
    {
        CardReleasedTiming,
        CardActivating,
        CardEndTiming
        //might more state like when card is drawed
    };
    public enum DamageTag
    {
        Damage,
        Break,
    };

    //virtual function
    public virtual void effectHolderDie()
    {
        Destroy(this.gameObject);
    }
    //control
    public abstract void cardInteraction();

    //abstract function
    public abstract void prefabInstantiater();
    public abstract void C_NewcolliderInstantiater();//used to create a special collider instead of the original one
    public abstract void AttackDamege();

    public abstract void EffectMessenger(string Dtype, int Dnum);

}
