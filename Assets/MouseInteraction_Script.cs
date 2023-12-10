using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MouseInteraction_Script : MonoBehaviour
{
    [Header("REFs")]
    public GameObject aimArea;
    public GameObject slashPath;
    public GameObject imageSlashPath; // first slash path, used to detect if slashable
    public GameObject player;
    // MOUSE DATAs
    Vector3 mouseDown_pos;
    Vector3 mouseCurrent_pos;
    public Vector3 mouseDir;
    [HideInInspector]
    public float mouseDrag_time;
    public float mouseDrag_maxTime;
    public bool dragging = false;

    public bool ifmouseCanUse = true;

    #region SINGLETON
    public static MouseInteraction_Script me;
    private void Awake()
    {
        me = this;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        mouseCurrent_pos = new();
        dragging = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ifmouseCanUse = false;
        }
        if (EffectsPostOfficeScript.me.PO_senderInfo.SenderCardState == "CardEndTiming")
        {
            ifmouseCanUse = true;
        }

        if (ifmouseCanUse)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        mouse3Dposition();
        if (Input.GetMouseButtonDown(0))
        {
            mouseDown_pos = mouseCurrent_pos; //Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        //mouseCurrent_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mouseCurrent_pos.z = 0;
        mouseDir = (mouseCurrent_pos - mouseDown_pos).normalized;
        if (!Input.GetMouseButton(0))
        {
            mouseDir = Vector3.zero;
        }
        //print(mouseDir);
    }

    private void mouse3Dposition()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePosOnScreen = Input.mousePosition;
        mousePosOnScreen.z = screenPos.z;
        mouseCurrent_pos = Camera.main.ScreenToWorldPoint(mousePosOnScreen);
        mouseCurrent_pos.y = 0;
    }

}
