using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this makes that all gameObject with this script get a box collider 2d automaticly
[RequireComponent(typeof(BoxCollider2D))]

public class ResourceNode : ToolHit
{
    //the gameObject that is going to drop
    [SerializeField] GameObject PickUpDrop;
    //the spread that the object will take from the object that has been destroyed
    [SerializeField] float spread = 0.7f;
    //the item that it is going to have the gameObject
    [SerializeField] Item item;
    //the amount of items that it is going to drop
    [SerializeField] int ItemCountInOneDrop = 1;
    [SerializeField] int Dropcount = 1;
    //the type of the object
    [SerializeField] ResourceNodeType nodeType;
    //the life of the object
    public int life = 100;


    public override void Golpe()
    {

        //checks the type of the object
        if (nodeType == ResourceNodeType.Bush)
        {
            //if the tool we are using is lvl 2 we are going to destroy the object
            if (GameManager.instance.player.GetComponent<ToolbarController>().GetItem.lvl == 2)
            {
                life = life - 100;
            }
            else
            {
                //if is not lvl2 we are dealing the damage of the item
                life = life - GameManager.instance.player.GetComponent<ToolbarController>().GetItem.Damage;
            }
        }
        else
        {
            //if the object is another type we only dealing the base damage of the object
            life = life - GameManager.instance.player.GetComponent<ToolbarController>().GetItem.Damage;
        }

        if (life <= 0) { Destroy(gameObject); }
        if (UnityEngine.Random.value <= 0.5) { return; }
        while (Dropcount > 0 && life <= 0)
        {
            Dropcount -= 1;

            Vector3 position = transform.position;
            position.x += spread * UnityEngine.Random.value - spread / 2;
            position.y += spread * UnityEngine.Random.value - spread / 2;

            ItemSpawnManager.instance.SpawnItem(position, item, ItemCountInOneDrop);

        }
    }

    public override bool CanBeHit(List<ResourceNodeType> canBeHit)
    {
        return canBeHit.Contains(nodeType);
    }

}
