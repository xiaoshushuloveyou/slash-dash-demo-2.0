using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hookerBoxScript : MonoBehaviour
{
    public Vector3 hookTargetPo;
    private Vector3 startPo;
    private Vector3 velocity = Vector3.zero;
    public float BoxslashSmooth = 1f;
    #region SINGLETON
    public static hookerBoxScript me;
    private void Awake()
    {
        me = this;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        startPo = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, hookTargetPo, ref velocity, BoxslashSmooth / 10f);
        if (Vector3.SqrMagnitude(transform.position - hookTargetPo) <= 0.005f)
        {
            EffectsPostOfficeScript.me.PO_senderInfo = new EffectsPostOfficeScript.senderState(EffectsPostOfficeScript.me.PO_senderInfo.SenderCardState,
                "CardEndTiming");
        }
    }
}
