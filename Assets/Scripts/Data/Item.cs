using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item")] 

public class Item : ScriptableObject
{
    public string Name;
    public int id;
    public bool Stackable;
    public Sprite Icon;
    public ToolAction OnAction;
    public ToolAction onTileMapAction;
    public ToolAction onItemUsed;
    public Crop crop;
    public bool iconHighlight;
    public GameObject itemPrefab;
    public int maxCapacity;
    public int capacity;
    public int priceToBuy = 10;
    public int priceToSell = 10;
    public bool canBeSold = true;
    public int Damage;
    public int lvl;

}    

