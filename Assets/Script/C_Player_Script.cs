using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class C_Player_Script : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Player_Move_Test")]//我们后面是没有自由移动的，这个移动wasd方便测试碰撞和测试动画用
    public float Player_Move_Test_speed = 3.0f;
    public bool nowisleftforward = true;
    public SPUM_Prefabs SPUM_Ani;

    [Header("Player_HealthBar")]//用于显示和控制怪物的血条
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
        Player_Move_controller();//用于触发移动

    }

    private void healthBarInstantiate()
    {
        //希望在怪物头上中间位置根据max血量数量，自动生成生命值的离散颗粒的序列。
        //希望血量是横向一条排列的，也许Boss体型大，头大能顶6颗血，但是一般目前的小怪物头上就顶3颗已经很多了。
        //max血量=2颗的时候矩阵位置不同，要居中
        Vector3 healthBarInsPos = new Vector3(transform.position.x - 0.05f, transform.position.y + 1.85f, transform.position.z + 1.85f);//记录第一个血条生成的头顶正上方的位置,世界坐标要算45度yz都要变
        if (C_HealthMaxNum == 2)
        {
            GameObject healthPoint_1 = Instantiate(C_HealthPoint, healthBarInsPos + new Vector3(0.4f, 0f, 0f), Quaternion.Euler(45f, 0f, 0f));
            GameObject healthPoint_2 = Instantiate(C_HealthPoint, healthBarInsPos + new Vector3(-0.4f, 0f, 0f), Quaternion.Euler(45f, 0f, 0f));
            healthPoint_1.transform.parent = gameObject.transform;//把生成的血量放到怪物下面，这样血条会跟着走
            healthPoint_2.transform.parent = gameObject.transform;
        }
        else
        {
            int ifleftspawnPoint = 1;
            float pointSpawnDis = 0f;
            for (int i = 0; i < C_HealthMaxNum; i++)//根据最大血量进行生成
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
        Player_Flip();//控制角色左右移动时的方向翻转
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
        }//控制移动的时候播放跑步动画，不动播放站立动画
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
