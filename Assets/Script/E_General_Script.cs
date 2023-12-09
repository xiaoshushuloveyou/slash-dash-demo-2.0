using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_General_Script : MonoBehaviour
{
    [Header("Enemy_HealthBar")]
    public GameObject E_HealthPoint;
    public int E_HealthMaxNum = 4;
    private int E_HealthCurrentNum;

    [Header("Enemy_BreakBar")]
    public int maxBreakNum = 4;
    public int currentBreakNum;
    public BreakBarScript breakBar;
    public V_E_BasicMovementLogic movementLogic;
    public Effect_CPU CPUscript;

    public bool ifhooked = false;

    // Start is called before the first frame update
    void Start()
    {
        E_HealthCurrentNum = E_HealthMaxNum;
        currentBreakNum = maxBreakNum;
        breakBar.SetMaxBreakNum(maxBreakNum);
        CPUscript.E_healthBarInstantiate(gameObject, E_HealthMaxNum, E_HealthPoint);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentBreakNum<0)
        {
            currentBreakNum = 0;
        }
        enemyselfDie();
        if (currentBreakNum <= 0)
        {
            CPUscript.E_BasicBreakEffect();
            if (!CPUscript.ifstopmove)
            {
                currentBreakNum = maxBreakNum;
                breakBar.SetBreakNum(currentBreakNum);
                
                //EffectsPostOfficeScript.me.PO_senderInfo = new EffectsPostOfficeScript.senderState(EffectsPostOfficeScript.me.PO_senderInfo.SenderCardState,"CardEndTiming");
            }
            
        }
        if (ifhooked)
        {
            CPUscript.E_hookerBoxEffectLogic(C_Player_Script.me.transform.position);
            ifhooked = CPUscript.ifhookboxover;
        }
    }
    private void enemyDamage(List<int> damageList)
    {
        for (int i = 0; i < damageList.Count; i++)
        {
            E_HealthMaxNum -= damageList[i];
        }
        
    }
    private void enemyBreakNum(List<int> breakNum)
    {
        for (int i = 0; i < breakNum.Count; i++)
        {
            currentBreakNum -= breakNum[i];
            breakBar.SetBreakNum(currentBreakNum);
        }

        
    }
    private void enemyselfDie()
    {
        if (E_HealthMaxNum<=0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DamageBox")
        {
            enemyDamage(CPUscript.E_DamageGoferLogic());
            //print("currentHealth:"+E_HealthCurrentNum);
        }
        if (other.tag == "BreakBox")
        {
            CPUscript.E_BasicBreakTimerReset();
            enemyBreakNum(CPUscript.E_BreakGoferLogic());
        }
        if (other.tag == "HookerBox")
        {
            ifhooked = true;
        }

    }

}
