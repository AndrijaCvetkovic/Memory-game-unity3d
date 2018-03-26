using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;

public class CardBehavior : MonoBehaviour {

    
    public Sprite cardFace;
    
    public Sprite cardBack;

    public Button but;
    public string Name;
    public bool state;
    public bool initialized = false;
    public int orederNumber;
    // Use this for initialization
    void Start()
    {
        but = GetComponent<Button>();
        but.GetComponent<Image>().sprite = cardBack;
    }

    public string cardName
    {
        get
        {
            return Name;
        }
        set
        {
            Name = value;
        }
    }

    public void ChangeSide()
    {
        if (!state)
        {
            but.GetComponent<Image>().sprite = cardFace;
            state = true;
        }

    }

    public void ChangeToBack()
    {

        but.GetComponent<Image>().sprite = cardBack;
        state = false;


    }
}
