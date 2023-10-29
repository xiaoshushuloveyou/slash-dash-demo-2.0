using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Button_Start_Script : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler
{

    public Button Button;
    public GameObject ButtonPlay;

    public void OnPointerDown(PointerEventData eventData)
    {
        ButtonPlay.transform.position -= new Vector3(-0.7f, 0.7f, 0);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ButtonPlay.transform.position += new Vector3(-0.7f, 0.7f, 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ButtonPlay.transform.position -= new Vector3(-0.7f, 0.7f, 0);
    }

    public void playGamePressed()
    {
        SceneManager.LoadScene(1);
    }

}