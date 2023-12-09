using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V_BasicSlash_PathHolder : MonoBehaviour
{
    public float pathGrowSpeed = 1f;
    public float pathLengthMax = 3f;

    #region SINGLETON
    public static V_BasicSlash_PathHolder me;
    private void Awake()
    {
        me = this;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MouseInteraction_Script.me.mouseDir != Vector3.zero)
        {
            pathGrow();
        }

        transform.position = new Vector3(C_Player_Script.me.transform.position.x,0.1f, C_Player_Script.me.transform.position.z);
        if (Input.GetMouseButtonUp(0))
        {
            Destroy(gameObject);
        }
    }
    private void pathGrow()
    {
        float angle = DotToAngle(new Vector3(0f, 0f, 1f), -MouseInteraction_Script.me.mouseDir);
        if (MouseInteraction_Script.me.mouseDir.x<0)
        {
            me.transform.eulerAngles = new Vector3(90f, angle, 0f);
        }
        else
        {
            me.transform.eulerAngles = new Vector3(90f, -angle, 0f);
        }
        basicPathGrow();
    }
    public void basicPathGrow()
    {
        if (transform.localScale.y <= pathLengthMax)
        {
            float newpathy = transform.localScale.y + Time.deltaTime * pathGrowSpeed;
            transform.localScale = new Vector3( 1f, newpathy, 1f);
        }
    }
    public static float DotToAngle(Vector3 _from, Vector3 _to)
    {
        float rad = 0;
        rad = Mathf.Acos(Vector3.Dot(_from.normalized, _to.normalized));
        return rad * Mathf.Rad2Deg;
    }
}
