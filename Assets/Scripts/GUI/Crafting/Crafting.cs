using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    [SerializeField] ItemContainer inventory;

    float timer = 1.5f;
    bool noObj;
    [SerializeField] GameObject Sign;

    private void Update() {
        
        if(noObj)
        {
            timer -= Time.deltaTime;
            Sign.SetActive(true);
            if(timer < 0)
            {
                noObj = false;
                timer = 1.5f;
                Sign.SetActive(false);
            }
        }
    }

    public void Craft(CraftingRecipe recipe)
    {
        //this see if we have space in the inventory
        if(inventory.CheckFreeSpace() == false)
        {
            Debug.Log("U dont have space in the inventory");
            noObj = true;
            return;
        }

        //this checks the item that we r going to craft if we have it in the inventory
        for(int i = 0; i < recipe.elements.Count; i++)
        {
            if(inventory.CheckItem(recipe.elements[i]) == false)
            {
                Debug.Log("U dont have the items in ur inventory");
                noObj = true;
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
