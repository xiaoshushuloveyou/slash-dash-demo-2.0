using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headbuttedScript : MonoBehaviour
{
    public GameObject HolderTarget;
    public float headbuttedPos=2f;
    public Vector3 SpawnPos;
    public Vector3 Spawnmenmery;
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
        if (!Input.GetMouseButton(0))
        {
            transform.position = C_Player_Script.me.transform.position+ new Vector3(Spawnmenmery.x, 0.8f, Spawnmenmery.z);
        }
    }
    private void pathGrow()
    {
        if (Input.GetMouseButton(0))
        {
            SpawnPos = C_Player_Script.me.transform.position - MouseInteraction_Script.me.mouseDir* headbuttedPos;
            Spawnmenmery = -MouseInteraction_Script.me.mouseDir * headbuttedPos;
        }
        transform.position = new Vector3(SpawnPos.x, 0.8f, SpawnPos.z);
        float angle = DotToAngle(new Vector3(0f, 0f, 1f), MouseInteraction_Script.me.mouseDir);
        if (MouseInteraction_Script.me.mouseDir.x < 0)
        {

            transform.eulerAngles = new Vector3(15f, -angle, 0f);
        }
        else
        {
            transform.eulerAngles = new Vector3(15f, angle, 0f);
        }
    }
    public static float DotToAngle(Vector3 _from, Vector3 _to)
    {
        float rad = 0;
        rad = Mathf.Acos(Vector3.Dot(_from.normalized, _to.normalized));
        return rad * Mathf.Rad2Deg;
    }
}
