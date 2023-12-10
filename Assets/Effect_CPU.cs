using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_CPU : MonoBehaviour
{
    public SPUM_Prefabs SPUM_Ani;
    public float breakTimeLength = 2f;
    public float breakTimeer = 0f;
    public bool CPUifnowisbreak = true;
    public bool ifstopmove = false;//send info to Enemy movement

    private Vector3 velocity = Vector3.zero;
    public float Smooth1 = 1f;

    public Vector3 halfdistoPlayer;
    public bool ifhookboxover = false;

    public float headButtedTimer = 0f;

    // Start is called before the first frame update
    public void E_healthBarInstantiate(GameObject effectholder,int E_HealthMaxNum, GameObject E_HealthPoint)
    {
        Vector3 healthBarInsPos = new Vector3(effectholder.transform.position.x - 0.05f, effectholder.transform.position.y + 1.85f, effectholder.transform.position.z + 1.85f);//记录第一个血条生成的头顶正上方的位置,世界坐标要算45度yz都要变
        if (E_HealthMaxNum == 2)
        {
            GameObject healthPoint_1 = Instantiate(E_HealthPoint, healthBarInsPos + new Vector3(0.4f, 0f, 0f), Quaternion.Euler(45f, 0f, 0f));
            GameObject healthPoint_2 = Instantiate(E_HealthPoint, healthBarInsPos + new Vector3(-0.4f, 0f, 0f), Quaternion.Euler(45f, 0f, 0f));
            healthPoint_1.transform.parent = effectholder.transform;//把生成的血量放到怪物下面，这样血条会跟着走
            healthPoint_2.transform.parent = effectholder.transform;
        }
        else
        {
            int ifleftspawnPoint = 1;
            float pointSpawnDis = 0f;
            float ifNumisSingle=0.4f;
            if ((E_HealthMaxNum&1)==1)
            {
                ifNumisSingle = 0;
            }
            for (int i = 0; i < E_HealthMaxNum; i++)//根据最大血量进行生成
            {
                GameObject healthPoint_n = Instantiate(E_HealthPoint, healthBarInsPos + new Vector3(pointSpawnDis * ifleftspawnPoint+ ifNumisSingle, 0f, 0f), Quaternion.Euler(45f, 0f, 0f));
                healthPoint_n.transform.parent = effectholder.transform;
                if (ifleftspawnPoint == 1)
                {
                    pointSpawnDis += 0.8f;
                }
                ifleftspawnPoint = -ifleftspawnPoint;
            }

        }
    }
    public List<int> E_DamageGoferLogic()
    {
        List<int> damamgeNumList = new List<int>();
        if (EffectsPostOfficeScript.me.PO_DamageBox.Count > 0)
        {
            for (int i = 0; i < EffectsPostOfficeScript.me.PO_DamageBox.Count; i++)
            {
                if (EffectsPostOfficeScript.me.PO_DamageBox[i].EffectType == "Damage")
                {
                    damamgeNumList.Add(EffectsPostOfficeScript.me.PO_DamageBox[i].Damage);
                }

            }
        }
        return damamgeNumList;
    }


    public List<int> E_BreakGoferLogic()
    {
        List<int> damamgeNumList = new List<int>();
        if (EffectsPostOfficeScript.me.PO_DamageBox.Count > 0)
        {
            for (int i = 0; i < EffectsPostOfficeScript.me.PO_DamageBox.Count; i++)
            {
                if (EffectsPostOfficeScript.me.PO_DamageBox[i].EffectType == "Break")
                {
                    damamgeNumList.Add(EffectsPostOfficeScript.me.PO_DamageBox[i].Damage);
                }

            }
        }
        return damamgeNumList;
    }

    public bool E_BasicBreakEffect()
    {
        
        if (CPUifnowisbreak)
        {
            ifstopmove = true;
            SPUM_Ani.PlayAnimation(0);
            breakTimeer += Time.deltaTime;
            if (breakTimeer > breakTimeLength)
            {
                CPUifnowisbreak = false;
                ifstopmove = false;
                breakTimeer = 0f;

            }
        }
        
        return CPUifnowisbreak;
    }
    public void E_BasicBreakTimerReset()
    {
        CPUifnowisbreak = true;
        breakTimeer = 0f;
        ifstopmove = false;
        headButtedTimer = 0f;

    }

    public void E_hookerBoxEffectLogic(Vector3 moveTarget)
    {
        if (!ifhookboxover)
        {
            halfdistoPlayer = GetBetweenPoint(transform.position, moveTarget);
            ifhookboxover = true;
        }
        transform.position = Vector3.SmoothDamp(transform.position, halfdistoPlayer, ref velocity, Smooth1/7f);
        //transform.position = GetBetweenPoint(transform.position, moveTarget.position);
        //print("YYYYYYYYYYYYYYYY");
        if (Vector3.SqrMagnitude(transform.position - halfdistoPlayer) <= 0.005f)
        {
            EffectsPostOfficeScript.me.PO_senderInfo = new EffectsPostOfficeScript.senderState(EffectsPostOfficeScript.me.PO_senderInfo.SenderCardState,"CardEndTiming");
            ifhookboxover = false;
        }
    }

    public void E_HeadbuttedEffectLogic( Vector3 hitpoint)
    {
        //be smashed
        //Vector3 hitpointtoPlayer = hitpoint - C_Player_Script.me.transform.position;
        transform.position = C_Player_Script.me.transform.position + hitpoint * 1.2f;
        headButtedTimer += Time.deltaTime;
    }

    public List<int> E_headbuttedDamageLogic()
    {
        List<int> returnvalue = new List<int>();
        //print(headButtedTimer);
        if (headButtedTimer<0.191f && headButtedTimer > 0.01f)
        {
            returnvalue.Add(1);
        }
        else if (headButtedTimer>0.2f)
        {
            returnvalue.Add(2);
        }
        else if (headButtedTimer <= 0.01f)
        {
            returnvalue.Add(0);
        }
        else
        {
            returnvalue.Add(3);
        }
        return returnvalue;
    }

    public void E_KnockedBackLogic()
    {
        Vector3 KnockbackDir = transform.position - C_Player_Script.me.transform.position;
        KnockbackDir = KnockbackDir.normalized;
        transform.position += KnockbackDir * 3f; 

    }

    /// <Private>
    /// 
    /// 
    private Vector3 GetBetweenPoint(Vector3 start, Vector3 end, float percent = 0.5f)
    {
        Vector3 normal = (end - start).normalized;
        float distance = Vector3.Distance(start, end);
        return normal * (distance * percent) + start;
    }
    private void Update()
    {
        if (EffectsPostOfficeScript.me.PO_senderInfo.SenderCardState == "CardEndTiming")
        {
            E_BasicBreakTimerReset();
            //
        }
    }
}
