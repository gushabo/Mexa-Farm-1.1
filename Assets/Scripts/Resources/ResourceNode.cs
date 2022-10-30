using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this makes that all gameObject with this script get a box collider 2d automaticly
[RequireComponent(typeof(BoxCollider2D))]

public class ResourceNode : ToolHit
{

    [SerializeField] GameObject PickUpDrop;
    [SerializeField] float spread = 0.7f;
    [SerializeField] Item item;
    [SerializeField] int ItemCountInOneDrop = 1;
    [SerializeField] int Dropcount = 1;
    [SerializeField] ResourceNodeType nodeType;

    public override void Golpe()
    {

        while(Dropcount > 0)
        {
            Dropcount -= 1;

            Vector3 position = transform.position;
            position.x += spread * UnityEngine.Random.value - spread / 2;
            position.y += spread * UnityEngine.Random.value - spread / 2;
            
            //GameObject go = Instantiate(PickUpDrop);
            //go.GetComponent<PickUpItem>().Set(item, ItemCountInOneDrop);
            //go.transform.position = position;
            
            ItemSpawnManager.instance.SpawnItem(position, item, ItemCountInOneDrop);
            
        }
        //If recieves a hit destroys the object
        Destroy(gameObject);
    }

    public override bool CanBeHit(List<ResourceNodeType> canBeHit)
    {
        return canBeHit.Contains(nodeType);
    }

}
