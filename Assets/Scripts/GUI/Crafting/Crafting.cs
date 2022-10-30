using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    [SerializeField] ItemContainer inventory;

    public void Craft(CraftingRecipe recipe)
    {
        //this see if we have space in the inventory
        if(inventory.CheckFreeSpace() == false)
        {
            Debug.Log("U dont have space in the inventory");
            return;
        }

        //this checks the item that we r going to craft if we have it in the inventory
        for(int i = 0; i < recipe.elements.Count; i++)
        {
            if(inventory.CheckItem(recipe.elements[i]) == false)
            {
                Debug.Log("U dont have the items in ur inventory");
                return;
            }
        }
       
        //this removes the cuantity of necessary items to craft
        for(int i=0; i < recipe.elements.Count; i++)
        {
            inventory.Remove(recipe.elements[i].item, recipe.elements[i].count);
        }
        
        inventory.Add(recipe.output.item, recipe.output.count);
    }
}
