using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour,IDropHandler
{

    public int SlotID=0;
    public GameObject Gamedata;

    public void Start()
    {
        
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            if (Gamedata.GetComponent<GameData>().Deck.Count <= SlotID)
            {
                //Gamedata.GetComponent<GameData>().Deck.Add(
                //eventData.pointerDrag.GetComponent<DeckDragDrop>().myname.ToString()
                //);
            }
            else
            {

                Gamedata.GetComponent<GameData>().Deck[SlotID] = eventData.pointerDrag.GetComponent<DeckDragDrop>().myname.GetHashCode();
            }
            Debug.Log(Gamedata.GetComponent<GameData>().Deck[0]+"____"
                + Gamedata.GetComponent<GameData>().Deck[1] + "____"
                + Gamedata.GetComponent<GameData>().Deck[2] + "____"
                + Gamedata.GetComponent<GameData>().Deck[3] + "____"
                + Gamedata.GetComponent<GameData>().Deck[4] + "____");
        }
        //else
        //{
        //    eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = eventData.pointerDrag.GetComponent<DeckDragDrop>().oriposition;
        //}
    }

}
