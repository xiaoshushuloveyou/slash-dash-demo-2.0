using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreakBarScript : MonoBehaviour
{
    public Slider breakslider;

    public void SetMaxBreakNum(int breakNum)
    {
        breakslider.maxValue = breakNum;
        breakslider.value = breakNum;
    }

    public void SetBreakNum(int breaNum)
    {
        breakslider.value = breaNum;
    }
}
