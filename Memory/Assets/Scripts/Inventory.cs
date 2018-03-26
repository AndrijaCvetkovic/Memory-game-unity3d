using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : ScriptableObject
{
    public List<ItemCard> cards;
    public AllCards allCards;
    /*public ItemCard takeRandomCard()
    {
        System.Random r = new System.Random();
        int idx = r.Next(1, cards.Count);
        ItemCard ret = cards[idx];
        cards.RemoveAt(idx);
        return cards[idx];
    }*/

    public void AddItem(Button i)
    {
        foreach (ItemCard ic in allCards.cards)
        {
            if (ic.name == i.GetComponent<ItemCardBehavior>().name)
            {
                cards.Add(ic);    
            }
        }
        
    }

    public void Reset()
    {
        cards = null;
    }

}
public class MakeItemCards
{
    [MenuItem("Assets/Create/Database")]
    public static void CreateDatabaseAsset()
    {
        Inventory data = ScriptableObject.CreateInstance<Inventory>();
        AssetDatabase.CreateAsset(data, "Assets/Resources/Inventory.asset");
        AssetDatabase.SaveAssets();
    }
}


