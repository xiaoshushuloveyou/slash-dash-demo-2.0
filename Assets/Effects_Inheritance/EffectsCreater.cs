using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EffectsCreater :MonoBehaviour
{
    //������Ϣ
    public string effectName;
    public int effectID;

    //ʹ����Ϣ
    public GameObject effectUser;//��ʾ˭ʹ�����Ч�������Ч��Ҫ��˭ʹ��
    public string effectReciever;//����Է���tag�����������Ҫ����Ч��������������tag��enemy����ô��Ҫ��enemy��������subeffects
    public int effectStartTimePoint;//Ч����ʼ���õ�ʱ��㣬������1����Slashǰ2����Slashʱ3����Slash������
    public List<ScriptableObject> subEffects= new List<ScriptableObject>();//����Я�������Ч,�������Ч������collider,�����ײ�廹��Ҫ��һ���˺�Ч��


    //��Ϊ�ӿ�
    public interface Effect3C//����ǽӿڣ����ڴ���Ч����3CӦ�����ͨ����ָ����
    {
        void effecctInteractive();//����ǽӿ�����Ľӿں���
    }

    public interface EffectIndicator//����ǽӿڣ����ڴ���Ч���ķ�Χ����Ϊָʾ���������Ե�Slash�ж���
    {
        void effectSilhouette();//Ҫע�ⲻ��ÿ�����ƶ����������ʾ����ͷ�����
    }
    public interface EffectBehavior//��ʹ�ó������ƺ������Ҫ��ʲô
    {
        void effectUserDo();//ʹ���ߵ������ó�����Ҫ��ʲô
        void subEffectDeliver();//��������˴���Ч���������������е������ˣ���ô����subeffects
    }

}
