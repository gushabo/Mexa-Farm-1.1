using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopOffers : Interactable
{
    public override void Interact(Character character)
    {
        ShopOffersCharacter shop = character.GetComponent<ShopOffersCharacter>();
        if(shop == null){return;}
        shop.begin(gameObject);
    }
}
