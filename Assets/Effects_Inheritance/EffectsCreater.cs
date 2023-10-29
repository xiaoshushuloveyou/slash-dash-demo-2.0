using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectsCreater :MonoBehaviour
{
    //基础信息
    public string effectName;
    public int effectID;

    //使用信息
    public GameObject effectUser;//表示谁使用这个效果，这个效果要给谁使用
    public string effectReciever;//如果对方的tag满足接收者需要传递效果。比如碰到了tag是enemy，那么就要给enemy接收器发subeffects
    public int effectStartTimePoint;//效果开始作用的时间点，比如是1代表Slash前2代表Slash时3代表Slash完成落地
    public List<ScriptableObject> subEffects= new List<ScriptableObject>();//用于携带别的特效,例如该特效是生成collider,这个碰撞体还需要带一个伤害效果


    //行为接口
    public interface Effect3C//这个是接口，用于处理效果的3C应该如何通过手指发生
    {
        void effecctInteractive();//这个是接口下面的接口函数
    }

    public interface EffectIndicator//这个是接口，用于处理效果的范围和行为指示，比如显性的Slash判定框
    {
        void effectSilhouette();//要注意不是每个卡牌都会带有有提示框的释放体验
    }
    public interface EffectBehavior//【使用出】卡牌后对象需要做什么
    {
        void effectUserDo();//使用者当卡牌用出后需要做什么
        void subEffectDeliver();//如果触发了传递效果的条件（比如切到敌人了）怎么传递subeffects
    }

}
