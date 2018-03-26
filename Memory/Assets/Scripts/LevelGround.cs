using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelGround : MonoBehaviour {


    public GameObject selected1;
    public GameObject selected2;
    public GameObject ground;
    public int counter;
    public int[] CardsArray;
    public System.Random ran = new System.Random();
    public List<GameObject> cardsOnField;
    public GameObject prefabCard;
    public GameObject cardForHint;
    public GameObject cardForHint1;
    public GameObject cardsHolder;
    public int pointsAtThisStage;
    public bool passed;
    public Sprite f1;
    public Sprite f2;

    public int lastHit;

    // Use this for initialization
    void Start()
    {

        cardsOnField = new List<GameObject>();
        CardsArray = new int[gameManager.instance.leveltoOpen.numberOfCards];
        GenerateField(gameManager.instance.leveltoOpen.numberOfCards);
        counter = 0;
        StartCoroutine("timer");
        lastHit = 0;
        pointsAtThisStage = 0;
        if(gameManager.instance.leveltoOpen.World == 2)
        {
            ground.GetComponent<Image>().sprite = f2;
        }

        int lv = PlayerPrefs.GetInt("Levels");

        if (lv <= gameManager.instance.leveltoOpen.orderNo)
        {
            passed = false;
        }
        else
        {
            passed = true;
        }
    }

    public Sprite cardretFace(int i)
    {
        if (i == 1)
        {
            return gameManager.instance.card1;
        }
        else if (i == 2)
        {
            return gameManager.instance.card2;
        }
        else if (i == 3)
        {
            return gameManager.instance.card3;
        }
        else if (i == 4)
        {
            return gameManager.instance.card4;
        }
        else if (i == 5)
        {
            return gameManager.instance.card5;
        }
        else if (i == 6)
        {
            return gameManager.instance.card6;
        }
        else if (i == 7)
        {
            return gameManager.instance.card7;
        }
        else if (i == 8)
        {
            return gameManager.instance.card8;
        }
        else
        {
            return gameManager.instance.card9;
        }
    }

    public void GenerateField(int numberOfCards)
    {
        int cardname = 1;
        int idx;

        for (int i = 0; i < numberOfCards; i = i + 2)
        {
            CardsArray[i] = cardname;
            CardsArray[i + 1] = cardname;
            cardname++;
        }

        //Random random = new Random();

        for (int i = 0; i < numberOfCards; i++)
        {
            idx = ran.Next(i, numberOfCards);

            //swap elements
            int tmp = CardsArray[i];
            CardsArray[i] = CardsArray[idx];
            CardsArray[idx] = tmp;
        }

        if(numberOfCards >= 10)
        {
            cardsHolder.GetComponent<GridLayoutGroup>().cellSize = new Vector2(50, 50);
        }

        for(int i = 0; i < numberOfCards; i++)
        {
            /*
            Button newb = Instantiate(prefabCard, ground.transform);
            newb.name = l.Name;
            newb.GetComponentInChildren<Text>().text = l.Name.ToString();
            if (l.Unlocked == false)
            {
                newb.GetComponent<Button>().interactable = false;
            }*/

            GameObject b = Instantiate(prefabCard, ground.transform);
            b.name = CardsArray[i].ToString();
            b.GetComponent<CardBehavior>().cardFace = cardretFace(CardsArray[i]);
            cardsOnField.Add(b);

        }
    }

    public void Clicked(GameObject card)
    {
        if (counter < 2)
        {
            if (counter == 0)
            {
                selected1 = card;
                selected1.GetComponent<CardBehavior>().ChangeSide();
                counter++;
                cardForHint = selected1;
                selected1.GetComponent<Image>().raycastTarget = false;
            }
            else if(counter == 1)
            {
                selected2 = card;
                selected2.GetComponent<CardBehavior>().ChangeSide();
                counter++;
                selected2.GetComponent<Image>().raycastTarget = false;
            }
        }

        if(counter == 2 && selected1!=null && selected2!=null)
        {
            StartCoroutine("DestroyOrTurn");
        }
    }

    IEnumerator DestroyOrTurn()
    {
        Debug.Log("Couroutine");
        yield return new WaitForSeconds(2);
        if (selected1.name == selected2.name)
        {
            if(lastHit!=0)
            {
                if(lastHit - gameManager.instance.timer < 5 && passed == false)
                {
                    gameManager.instance.score += 5;
                    pointsAtThisStage += 5;
                }
            }
            cardsOnField.Remove(selected1);
            cardsOnField.Remove(selected2);
            selected1.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            selected2.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            selected1.GetComponent<Image>().raycastTarget = false;
            selected2.GetComponent<Image>().raycastTarget = false;
            selected1 = null;
            selected2 = null;
            lastHit = gameManager.instance.timer;
            if (passed == false)
            {
                gameManager.instance.score += 10;
                pointsAtThisStage += 10;
            }
        }
        else
        {
            selected1.GetComponent<Image>().raycastTarget = true;
            selected2.GetComponent<Image>().raycastTarget = true;
            selected1.GetComponent<CardBehavior>().ChangeToBack();
            selected2.GetComponent<CardBehavior>().ChangeToBack();
            selected1 = null;
            selected2 = null;
        }

        if(cardsOnField.Count == 0)
        {
            int lv = PlayerPrefs.GetInt("Levels");
            //if (gameManager.instance.levelsData[gameManager.instance.leveltoOpen.orderNo - 1].passed == false)
            if (lv <= gameManager.instance.leveltoOpen.orderNo)
            {
                
                gameManager.instance.levelsPassed++;
                PlayerPrefs.SetInt("Levels", gameManager.instance.levelsPassed);
                gameManager.instance.levelsData[gameManager.instance.leveltoOpen.orderNo - 1].passed = true;
                int points = PlayerPrefs.GetInt("Score");
                points = points + pointsAtThisStage;
                PlayerPrefs.SetInt("Score", points);
                
            }
            //gameManager.instance.UnlockNextLevel(gameManager.instance.leveltoOpen.orderNo, gameManager.instance.leveltoOpen);
            SceneManager.LoadScene("Menu");
            
        }
        counter = 0;
    }

    IEnumerator timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            gameManager.instance.timer--;
            if(gameManager.instance.timer == 0)
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }

    public void Hint()
    {
        if (counter == 1)
        {
            for (int i = 0; i < cardsOnField.Count; i++)
            {
                if (cardsOnField[i].name == selected1.name && cardsOnField.IndexOf(cardsOnField[i]) != cardsOnField.IndexOf(selected1))
                {
                    cardForHint1 = cardsOnField[i];
                    StartCoroutine("hint");


                }
            }
        }

    }

    IEnumerator hint()
    {
        if (passed == false)
        {
            gameManager.instance.score -= 3;
            pointsAtThisStage -= 3;
        }
        cardForHint1.GetComponent<CardBehavior>().ChangeSide();
        yield return new WaitForSeconds(1);
        cardForHint1.GetComponent<CardBehavior>().ChangeToBack();
        

    }

}


