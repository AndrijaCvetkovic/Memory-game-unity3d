using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour {

    public GameObject inv;
    public GameObject shop;

    public Button invCard;
    public Button shopCard;

    public Inventory i;
    public AllCards allc;

   public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

	// Use this for initialization
	void Start () {
        i.cards = new List<ItemCard>();
        AddCardToInv();
        Shop();


        //for test
        //PlayerPrefs.SetInt("Score", 500);
    }

    public void Inv()
    {

    }

    public void Shop()
    {
        foreach(ItemCard it in allc.cards)
        {
            if (!i.cards.Contains(it))
            {
                Button newb = Instantiate(shopCard, shop.transform);
                newb.name = it.name;
                newb.GetComponent<ItemCardBehavior>().itemcard = it;
                newb.GetComponent<Image>().sprite = it.image;
            
                
            }
        }
    }

    public void AddCardToInv()
    {
        string car = PlayerPrefs.GetString("Cards");
        string[] names = car.Split(' ');

        foreach (ItemCard it in allc.cards)
        {
            for (int ig = 0; ig < names.Length; ig++)
            {
                if (names[ig] == it.name)
                {
                    Button newb = Instantiate(invCard, inv.transform);
                    newb.name = it.name;
                    newb.GetComponent<ItemCardBehavior>().itemcard = it;
                    newb.GetComponent<Image>().sprite = it.image;
                    i.cards.Add(it);
                }
            }

        }
    }

    public void buyCard(GameObject g)
    {
        int score = PlayerPrefs.GetInt("Score");
        if (score > 50)
        {
            i.cards.Add(g.GetComponent<ItemCardBehavior>().itemcard);
            Button newb = Instantiate(invCard, inv.transform);
            newb.name = g.GetComponent<ItemCardBehavior>().itemcard.name;
            newb.GetComponent<ItemCardBehavior>().itemcard = g.GetComponent<ItemCardBehavior>().itemcard;
            newb.GetComponent<Image>().sprite = g.GetComponent<ItemCardBehavior>().itemcard.image;
            Destroy(g);
            score -= 50;
            PlayerPrefs.SetInt("Score", score);
            string c = PlayerPrefs.GetString("Cards");
            PlayerPrefs.SetString("Cards",c + newb.GetComponent<ItemCardBehavior>().itemcard.name + ' ');
            gameManager.instance.score = score;
            gameManager.instance.cards = c + newb.GetComponent<ItemCardBehavior>().itemcard.name + ' ';
        }
    }

    public void SellCard(GameObject g)
    {
        int score = PlayerPrefs.GetInt("Score");
        allc.cards.Add(g.GetComponent<ItemCardBehavior>().itemcard);
        i.cards.Remove(g.GetComponent<ItemCardBehavior>().itemcard);
        Button newb = Instantiate(shopCard, shop.transform);
        newb.name = g.GetComponent<ItemCardBehavior>().itemcard.name;
        newb.GetComponent<ItemCardBehavior>().itemcard = g.GetComponent<ItemCardBehavior>().itemcard;
        newb.GetComponent<Image>().sprite = g.GetComponent<ItemCardBehavior>().itemcard.image;
        Destroy(g);
        score += 50;
        PlayerPrefs.SetInt("Score", score);
        gameManager.instance.score = score;
        string s = string.Empty;
        foreach (ItemCard ic in i.cards)
        {
            s += ic.name + ' ';
        }
        PlayerPrefs.SetString("Cards", s);
        gameManager.instance.cards = s;
       
    }


}
