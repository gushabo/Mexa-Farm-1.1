using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    //variables to the items that are being pick up
    Transform player;
    [SerializeField] float speed = 5f;
    [SerializeField] float PickUpDistance = 1.5f;
    //this is the variable to destroy the object after a certain time
    //[SerializeField] float ttl = 20f;

    //variables to add the items in the inventory
    public Item item;
    public int count = 1;

    private void Awake() { 
        //this gives us the position of the player
        player = GameManager.instance.player.transform;
    }

    //this set the item, count and the sprite on the new space we are trying to take
    public void Set(Item item, int count)
    {
        this.item = item;
        this.count = count;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = item.Icon;
    }

    private void Update()
    {
        /*this makes the time to destroy the object
        ttl -= Time.deltaTime;
        if(ttl < 0)
        {
            Destroy(gameObject);
        }
        */

        //this gives the position of the item that are drop
        float distance = Vector3.Distance(transform.position,player.position);
        if(distance > PickUpDistance)
        {
            return;
        }

        //this makes that the object move to the player
        transform.position = Vector3.MoveTowards(transform.position,player.position, speed * Time.deltaTime);

        if(distance < 0.1f)
        {
            //this is going to be changed in a specific scripts cause is better to be checked in another one than in these that is generalized
            if(GameManager.instance.InventoryContainer != null)
            {
                GameManager.instance.InventoryContainer.Add(item, count);
            }else {
                Debug.LogWarning("no inventory container in the game manager");
            }
            Destroy(gameObject);
        }

    }
    
}