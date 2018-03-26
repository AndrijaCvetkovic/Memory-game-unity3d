using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour {

    public static gameManager instance { get; private set; }

    public Sprite card1;
    public Sprite card2;
    public Sprite card3;
    public Sprite card4;
    public Sprite card5;
    public Sprite card6;
    public Sprite card7;
    public Sprite card8;
    public Sprite card9;
    public Sprite cardBack;
    public Button b;
    public GameObject scrollList;
    public int timer;
    public string cards;

    public Level leveltoOpen;

    public int score;
    public int levelsPassed;

    public struct Player
    {
        public string points { get; set; }
    }

    [System.Serializable]
    public class Level
    {
        public string Name;
        public int orderNo;
        public int pointsToOpen;
        public int numberOfCards;
        public int playerScore;
        public int World;
        public bool passed;
        public bool Unlocked;
        public int time;
    }

    public List<Button> levelsButtons;
    public List<Level> levelsData;
    public List<ItemCard> items;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        //InportLevels();
        //leveltoOpen = levelsData[1];
        // LoadDataLevels();
        levelsPassed = PlayerPrefs.GetInt("Levels");
        if(levelsPassed == 0)
        {
            levelsPassed = 1;
        }
        cards = PlayerPrefs.GetString("Cards");
    }



    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void OnGUI()
    {
        
        GUI.skin.label.fontSize = 30;
        GUI.contentColor = Color.red;
        GUI.Label(new Rect(20, 0, 150, 100), "Score: " + score);

        GUI.Label(new Rect(170, 0, 150, 100), "Timer: " + timer);
    }

    /*
    public void InportLevels()
    {
        foreach (Level l in levelsData)
        {
            Button newb = Instantiate(b,scrollList.transform);
            newb.name = l.Name;
            newb.GetComponentInChildren<Text>().text = l.Name.ToString();
            if (l.Unlocked == false)
            {
                newb.GetComponent<Button>().interactable = false;

            }


        }
    }*/

    public void UnlockNextLevel(int i,Level lev)
    {
        Level pom;
        int no = i + 1;
        /*foreach(Level l in levelsData)
        {
            if (l.orderNo == no)
            {
                pom = l;
                
            }
        }*/
       // levelsData[levelsData.IndexOf(lev) + 1].Unlocked = true;
       // pom = levelsData.ElementAt(lev.orderNo+1);
        //pom.Unlocked = true;
        levelsData[lev.orderNo].Unlocked = true;
      //  pom.Unlocked = true;
       

       
    }
}
