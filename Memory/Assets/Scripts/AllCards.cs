using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AllCards : ScriptableObject
{

  public List<ItemCard> cards;

}
public class MakeDatabaseForAllCards
{
    [MenuItem("Assets/Create/DatabaseForAllCards")]
    public static void CreateDatabaseAsset()
    {
        AllCards dataAllCards = ScriptableObject.CreateInstance<AllCards>();
        AssetDatabase.CreateAsset(dataAllCards, "Assets/Resources/AllCards.asset");
        AssetDatabase.SaveAssets();
    }

    

}
