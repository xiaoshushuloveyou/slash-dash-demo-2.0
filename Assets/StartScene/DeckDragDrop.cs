using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeckDragDrop : MonoBehaviour, IPointerDownHandler,IBeginDragHandler,IEndDragHandler,IDragHandler,IDropHandler
{
    public enum CardName
    {
        BasicSlash = 0,
        BreakSlash =1,
        HookOver = 2,
        HeadButted = 3,
    };

    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Image image;

    public GameObject selfCard;

    public bool ifstartdragging = true;
    //public bool ifdesdroy;
    public Vector3 oriposition;

    public CardName myname;

    private void Awake()
    {
        
    }

    public void Start()
    {
        oriposition = transform.parent.transform.InverseTransformPoint(transform.position);
        Quaternion quaternion = Quaternion.Inverse(transform.parent.transform.rotation) * transform.rotation;
        //Debug.Log(oriposition);
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.color  = Color.red;
        canvasGroup.alpha = 0.3f;
        canvasGroup.blocksRaycasts = false;
        

    }

    public void OnDrag(PointerEventData eventData)
    {
        //if (ifstartdragging)
        //{
        //    GameObject insSelf = Instantiate(selfCard);
        //    insSelf.transform.SetParent(gameObject.transform.parent);
        //    insSelf.transform.position = transform.position;
        //    ifstartdragging = false;
        //}
        rectTransform.anchoredPosition += eventData.delta/ canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        image.color = Color.white;
        canvasGroup.blocksRaycasts = true;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("What on dorp?");
        //if (eventData.pointerDrag == null)
        //{
        //    Debug.Log("Destroy");
        //    Destroy(gameObject);
        //}
    }
}
