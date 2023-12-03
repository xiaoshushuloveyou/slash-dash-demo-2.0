using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class C_Player_Script : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Player_Move_Test")]//���Ǻ�����û�������ƶ��ģ�����ƶ�wasd���������ײ�Ͳ��Զ�����
    public float Player_Move_Test_speed = 3.0f;
    public bool nowisleftforward = true;
    public SPUM_Prefabs SPUM_Ani;

    [Header("Player_HealthBar")]//������ʾ�Ϳ��ƹ����Ѫ��
    public GameObject C_HealthPoint;
    public int C_HealthMaxNum = 3;
    private int C_HealthCurrentNum;

    #region SINGLETON
    public static C_Player_Script me;
    private void Awake()
    {
        me = this;
    }
    #endregion

    void Start()
    {
        C_HealthCurrentNum = C_HealthMaxNum;
        healthBarInstantiate();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Player_Move_controller();//���ڴ����ƶ�

    }

    private void healthBarInstantiate()
    {
        //ϣ���ڹ���ͷ���м�λ�ø���maxѪ���������Զ���������ֵ����ɢ���������С�
        //ϣ��Ѫ���Ǻ���һ�����еģ�Ҳ��Boss���ʹ�ͷ���ܶ�6��Ѫ������һ��Ŀǰ��С����ͷ�ϾͶ�3���Ѿ��ܶ��ˡ�
        //maxѪ��=2�ŵ�ʱ�����λ�ò�ͬ��Ҫ����
        Vector3 healthBarInsPos = new Vector3(transform.position.x - 0.05f, transform.position.y + 1.85f, transform.position.z + 1.85f);//��¼��һ��Ѫ�����ɵ�ͷ�����Ϸ���λ��,��������Ҫ��45��yz��Ҫ��
        if (C_HealthMaxNum == 2)
        {
            GameObject healthPoint_1 = Instantiate(C_HealthPoint, healthBarInsPos + new Vector3(0.4f, 0f, 0f), Quaternion.Euler(45f, 0f, 0f));
            GameObject healthPoint_2 = Instantiate(C_HealthPoint, healthBarInsPos + new Vector3(-0.4f, 0f, 0f), Quaternion.Euler(45f, 0f, 0f));
            healthPoint_1.transform.parent = gameObject.transform;//�����ɵ�Ѫ���ŵ��������棬����Ѫ���������
            healthPoint_2.transform.parent = gameObject.transform;
        }
        else
        {
            int ifleftspawnPoint = 1;
            float pointSpawnDis = 0f;
            for (int i = 0; i < C_HealthMaxNum; i++)//�������Ѫ����������
            {
                GameObject healthPoint_n = Instantiate(C_HealthPoint, healthBarInsPos + new Vector3(pointSpawnDis * ifleftspawnPoint, 0f, 0f), Quaternion.Euler(45f, 0f, 0f));
                healthPoint_n.transform.parent = gameObject.transform;
                if (ifleftspawnPoint == 1)
                {
                    pointSpawnDis += 0.8f;
                }
                ifleftspawnPoint = -ifleftspawnPoint;
            }

        }
    }


    private void Player_Move_controller()
    {
        Player_Flip();//���ƽ�ɫ�����ƶ�ʱ�ķ���ת
        #region Basic Movement Controller
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, 1f, 1f) * Time.deltaTime * Player_Move_Test_speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, -1f, -1f) * Time.deltaTime * Player_Move_Test_speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(-1f, 0, 0) * Time.deltaTime * Player_Move_Test_speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(1f, 0, 0) * Time.deltaTime * Player_Move_Test_speed);
        }
        #endregion
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            SPUM_Ani.PlayAnimation(1);
        }
        else
        {
            SPUM_Ani.PlayAnimation(0);
        }//�����ƶ���ʱ�򲥷��ܲ���������������վ������
    }
    void Player_Flip()
    {
        float faceDir = MouseInteraction_Script.me.mouseDir.x;
        if (faceDir<0)
        {
            transform.localScale = new Vector3(-1, 1, 1) * 3f;
        }
        if (faceDir > 0)
        {
            transform.localScale = new Vector3(1, 1, 1) * 3f;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy"&& EffectsPostOfficeScript.me.PO_senderInfo.SenderHolderState!="Moving")
        {
            //print("player"+EffectsPostOfficeScript.me.PO_senderInfo.SenderHolderState);
            SceneManager.LoadScene(2);
            
        }
        
    }

}
