using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V_E_BasicMovementLogic : MonoBehaviour
{
    [Header("Enemy_Move")]//用于敌人的移动控制
    private Vector3 E_moveDir;
    public float E_moveSpeed = 2.0f;

    [Header("Enemy_Ani")]//用于控制怪物的跑步动作，近的时候会跑步快一点
    public SPUM_Prefabs SPUM_Ani;
    private float E_Status_Range = 6f;
    private float E_OriSpeed;

    public bool ifnowcanMove = true;
  

    public Effect_CPU CPUScript;


    // Start is called before the first frame update
    void Start()
    {
        E_OriSpeed = E_moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CPUScript.ifstopmove)
        {
            enemy_Movement();
            enemy_Flip();
            enemy_Status();
        }

    }
    private void enemy_Status()
    {
        float E_playerDis = E_moveDir.magnitude;
        if (E_playerDis <= E_Status_Range)
        {
            E_moveSpeed = 2f * E_OriSpeed;
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
        if (faceDir < 0)
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
}
