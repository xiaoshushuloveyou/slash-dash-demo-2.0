using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V_BasicSlash_PathCollision : MonoBehaviour
{

    private Color OriColor;
    public Vector3 childpathEndPointPo;
    public List<GameObject> enemiesEffected;

    #region SINGLETON
    public static V_BasicSlash_PathCollision me;
    private void Awake()
    {
        me = this;
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        OriColor = this.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesEffected.Count==0)
        {
            this.GetComponent<Renderer>().material.color = OriColor;
        }

        Transform childTransform = transform.Find("path End Point");
        childpathEndPointPo = childTransform.TransformPoint(Vector3.zero);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Enemy")
        {
            this.GetComponent<Renderer>().material.color = new Vector4(255.0f / 255, 126f / 255, 126f / 255, 1f);
            enemiesEffected.Add(other.gameObject);

        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemiesEffected.Remove(other.gameObject);

        }
    }

}
