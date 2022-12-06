using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Tool Action/Eat")]
public class Eat : ToolAction
{

    public override bool OnApply(Vector2 worldPoint)
    {
        return base.OnApply(worldPoint);
    }

    public override void OnItemUsed(Item usedItem, ItemContainer inventory)
    {
        Debug.Log("se usa XD");
        GameManager.instance.player.GetComponent<Character>().stamina.Add(usedItem.capacity);
        inventory.Remove(usedItem);
    }

}
