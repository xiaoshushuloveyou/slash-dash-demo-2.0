using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_General_Script : MonoBehaviour
{
    //����ű����������Ƶ��˵�ͨ����Ϊ��
    //ע��ͨ�õĽű���������singleton

    [Header("Enemy_Move")]//���ڵ��˵��ƶ�����
    private Vector3 E_moveDir;
    public float E_moveSpeed = 2.0f;

    [Header("Enemy_Ani")]//���ڿ��ƹ�����ܲ�����������ʱ����ܲ���һ��
    public SPUM_Prefabs SPUM_Ani;
    private float E_Status_Range = 6f;
    private float E_OriSpeed;

    [Header("Enemy_HealthBar")]//������ʾ�Ϳ��ƹ����Ѫ��
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
        //ϣ���ڹ���ͷ���м�λ�ø���maxѪ���������Զ���������ֵ����ɢ���������С�
        //ϣ��Ѫ���Ǻ���һ�����еģ�Ҳ��Boss���ʹ�ͷ���ܶ�6��Ѫ������һ��Ŀǰ��С����ͷ�ϾͶ�3���Ѿ��ܶ��ˡ�
        //maxѪ��=2�ŵ�ʱ�����λ�ò�ͬ��Ҫ����
        Vector3 healthBarInsPos = new Vector3(transform.position.x-0.05f, transform.position.y + 1.85f, transform.position.z+1.85f);//��¼��һ��Ѫ�����ɵ�ͷ�����Ϸ���λ��,��������Ҫ��45��yz��Ҫ��
        if (E_HealthMaxNum == 2)
        {
            GameObject healthPoint_1 = Instantiate(E_HealthPoint, healthBarInsPos+new Vector3(0.4f,0f,0f), Quaternion.Euler(45f, 0f, 0f));
            GameObject healthPoint_2 = Instantiate(E_HealthPoint, healthBarInsPos + new Vector3(-0.4f, 0f, 0f), Quaternion.Euler(45f, 0f, 0f));
            healthPoint_1.transform.parent = gameObject.transform;//�����ɵ�Ѫ���ŵ��������棬����Ѫ���������
            healthPoint_2.transform.parent = gameObject.transform;
        }
        else
        {
            int ifleftspawnPoint = 1;
            float pointSpawnDis = 0f;
            for (int i = 0; i < E_HealthMaxNum; i++)//�������Ѫ����������
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
        //���ݹ����player�����λ�ü��㳯��
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
