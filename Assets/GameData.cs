using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public List<int> Deck;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    public void Start()
    {
        Deck = new List<int>() { -1,-1,-1,-1,-1 };
    }

}
