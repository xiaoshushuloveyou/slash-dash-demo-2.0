using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoxScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        desdroyselfact();
    }

    public void desdroyselfact()
    {
        if (EffectsPostOfficeScript.me.PO_senderInfo.SenderCardState == "CardEndTiming")
        {
            Destroy(gameObject);
        }
    }
}
