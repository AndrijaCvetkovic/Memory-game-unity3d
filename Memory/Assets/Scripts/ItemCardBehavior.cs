using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCardBehavior : MonoBehaviour {

    public string name;
    public Sprite s;
    public GameObject b;
    public ItemCard itemcard;

    public void takePicture()
    {
        b.GetComponent<Button>().GetComponent<Image>().sprite = s;
    }

}
