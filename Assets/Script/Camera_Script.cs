using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour
{
    [Header("Camera_Follow")]//用于摄像机跟着角色走
    private Vector3 offset = new Vector3(0f, 0f, 0f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPosiiton = target.position+ offset;
        transform.position = Vector3.SmoothDamp(transform.position,targetPosiiton, ref velocity, smoothTime);
    }
}
