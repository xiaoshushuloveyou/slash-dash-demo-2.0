using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_General_Script : MonoBehaviour
{
    //这个脚本是用来控制敌人的通用行为的
    //注意通用的脚本不可以用singleton

    [Header("Enemy_Move")]//用于敌人的移动控制
    private Vector3 E_moveDir;
    public float E_moveSpeed = 2.0f;

    [Header("Enemy_Ani")]//用于控制怪物的跑步动作，近的时候会跑步快一点
    public SPUM_Prefabs SPUM_Ani;
    private float E_Status_Range = 6f;
    private float E_OriSpeed;

    [Header("Enemy_HealthBar")]//用于显示和控制怪物的血条
    public GameObject E_HealthPoint;
    public int E_HealthMaxNum = 3;
    private int E_HealthCurrentNum;


    // Start is called before the first frame update
    void Start()
    {

        E_OriSpeed = E_moveSpeed;

        E_HealthCurrentNum = E_HealthMaxNum;
        healthBarInstantiate();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        enemy_Movement();
        enemy_Flip();
        enemy_Status();
        enemyselfDie();
    }

    private void healthBarInstantiate()
    {
        //希望在怪物头上中间位置根据max血量数量，自动生成生命值的离散颗粒的序列。
        //希望血量是横向一条排列的，也许Boss体型大，头大能顶6颗血，但是一般目前的小怪物头上就顶3颗已经很多了。
        //max血量=2颗的时候矩阵位置不同，要居中
        Vector3 healthBarInsPos = new Vector3(transform.position.x-0.05f, transform.position.y + 1.85f, transform.position.z+1.85f);//记录第一个血条生成的头顶正上方的位置,世界坐标要算45度yz都要变
        if (E_HealthMaxNum == 2)
        {
            GameObject healthPoint_1 = Instantiate(E_HealthPoint, healthBarInsPos+new Vector3(0.4f,0f,0f), Quaternion.Euler(45f, 0f, 0f));
            GameObject healthPoint_2 = Instantiate(E_HealthPoint, healthBarInsPos + new Vector3(-0.4f, 0f, 0f), Quaternion.Euler(45f, 0f, 0f));
            healthPoint_1.transform.parent = gameObject.transform;//把生成的血量放到怪物下面，这样血条会跟着走
            healthPoint_2.transform.parent = gameObject.transform;
        }
        else
        {
            int ifleftspawnPoint = 1;
            float pointSpawnDis = 0f;
            for (int i = 0; i < E_HealthMaxNum; i++)//根据最大血量进行生成
            {
                GameObject healthPoint_n = Instantiate(E_HealthPoint, healthBarInsPos+ new Vector3(pointSpawnDis * ifleftspawnPoint, 0f, 0f), Quaternion.Euler(45f,0f,0f));
                healthPoint_n.transform.parent = gameObject.transform;
                if (ifleftspawnPoint==1)
                {
                    pointSpawnDis += 0.8f;
                }
                ifleftspawnPoint = -ifleftspawnPoint;
            }
            
        }
    }

    private void enemy_Status()
    {
        float E_playerDis = E_moveDir.magnitude;
        if (E_playerDis<= E_Status_Range)
        {
            E_moveSpeed = 2f* E_OriSpeed;
            SPUM_Ani.PlayAnimation(1);
        }
        else
        {
            E_moveSpeed = E_OriSpeed;
            SPUM_Ani.PlayAnimation(0);
        }
    }

    private void enemy_Movement()
    {
        E_moveDir = C_Player_Script.me.transform.position - transform.position;
        Vector3 E_moveDir_Nor = E_moveDir.normalized;
        transform.position += E_moveDir.normalized * E_moveSpeed * Time.deltaTime;
    }
    void enemy_Flip()
    {
        //根据怪物和player的相对位置计算朝向
        float faceDir = C_Player_Script.me.transform.position.x - transform.position.x;
        if (faceDir < 0 )
        {
            faceDir = -1f;
        }
        else
        {
            faceDir = 1f;
        }
        if (faceDir != 0)
        {
            transform.localScale = new Vector3(-faceDir, 1, 1) * 3f;
        }
    }

    public void enemyGofer()
    {
        if (EffectsPostOfficeScript.me.PO_DamageBox.Count>0)
        {
            for (int i = 0; i < EffectsPostOfficeScript.me.PO_DamageBox.Count; i++)
            {
                switch (EffectsPostOfficeScript.me.PO_DamageBox[i].EffectType)
                {
                    case "Damage":
                        
                        enemyDamage(EffectsPostOfficeScript.me.PO_DamageBox[i].Damage);
                        break;
                    case "Break":
                        break;
                    default:
                        break;
                }
            }
 
        }
        
    }
    private void enemyDamage(int Dnum)
    {
        E_HealthMaxNum -= Dnum;
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
        if (other.tag == "Hero")
        {
            enemyGofer();
        }
    }

}
