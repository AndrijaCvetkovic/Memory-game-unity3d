using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour {

    public Button b;
    public GameObject scrollList;
    public Button w2;
    public Button w1;
    public int timer;
    public Sprite w1l;
    public Sprite w2l;

    public gameManager.Level leveltoOpen;
   

    public void Start()
    {
        InportLevels();
        gameManager.instance.timer = 0;
        gameManager.instance.score = PlayerPrefs.GetInt("Score");
        
    }

    public void RestertLevels()
    {
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("Levels", 1);
        gameManager.instance.levelsPassed = 1;
        SceneManager.LoadScene("Menu");
        PlayerPrefs.SetString("Cards", String.Empty);

    }

    public void InportLevels()
    {
        int pomlevelsPassed = gameManager.instance.levelsPassed;
        int cardBr = 1;
        int levelCounter = 1;
        Sprite wl = w1l;
        foreach (gameManager.Level l in gameManager.instance.levelsData)
        {
            
            if(l.World == 1)
            {
                b = w1;
                wl = w1l;
            }
            else if(l.World == 2)
            {
                b = w2;
                wl = w2l;
            }

            Button newb = Instantiate(b, scrollList.transform);
            newb.name = l.Name;
            newb.GetComponentInChildren<Text>().text = l.Name.ToString();
            //newb.GetComponent<CardBehavior>().orederNumber = cardBr;
            cardBr++;
            if (pomlevelsPassed >= levelCounter)
            {
                l.Unlocked = true;
                newb.GetComponent<Button>().interactable = true;
                
            }
            else
            {
                l.Unlocked = false;
                newb.GetComponent<Button>().interactable = false;
                newb.GetComponent<Button>().GetComponent<Image>().sprite = wl;
            }


            levelCounter++;

        }
    }

    public void Shop()
    {
        SceneManager.LoadScene("InvShop");
    }

    public void CreateScene(GameObject gb)
    {

        foreach (gameManager.Level l in gameManager.instance.levelsData)
        {
            if (l.Name == gb.name)
            {
                gameManager.instance.leveltoOpen = l;
                gameManager.instance.timer = l.time;
            }
        }
        SceneManager.LoadScene("Level");
    }
    /*
    public void UnlockScenes()
    {
        int pomlevelsPassed = gameManager.instance.levelsPassed;
        

        for (int i=0;i<gameManager.instance.levelsData.Count;i++)
        {
            if(pomlevelsPassed > 0)
            {
                gameManager.instance.levelsData[i].Unlocked = true;
            }
        }
    }
    */
}
