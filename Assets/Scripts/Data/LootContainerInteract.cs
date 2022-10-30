using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootContainerInteract : Interactable
{

    [SerializeField] GameObject CloseChest;
    [SerializeField] GameObject OpenChest;
    [SerializeField] bool open;
    [SerializeField] AudioClip onOpenAudio;
    [SerializeField] public ItemContainer itemContainer;
    public bool upgrade;


    public override void Interact(Character character)
    {
        if (open == false)
        {
           Open(character);
        }
        else
        {
           Close(character);
        }
    }

    public void Open(Character character)
    {
        open = true;
        CloseChest.SetActive(false);
        OpenChest.SetActive(true);
        AudioManager.instance.Play(onOpenAudio);
        character.GetComponent<ItemContainerInteractController>().Open(itemContainer, transform);
    }

    public void Close(Character character)
    {
        open = false;
        CloseChest.SetActive(true);
        OpenChest.SetActive(false);
        character.GetComponent<ItemContainerInteractController>().Close();
    }

}
