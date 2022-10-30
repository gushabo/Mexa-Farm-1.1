using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractController : MonoBehaviour
{
    CharacterController2D characterController;
    Rigidbody2D rgbd2D;
    Character character;
    //Create this new 2 fields to use them as the distance
    [SerializeField] float OffSetDistance = 1f;
    [SerializeField] float SizeOfInteractableArea = 1.2f;
    //this is to use the highlight script that originally goes in the game manager
    //[SerializeField] HighLightController highlightController;

    private void Awake() {
        characterController = GetComponent<CharacterController2D>();
        rgbd2D = GetComponent<Rigidbody2D>();
        character = GetComponent<Character>();
    }

    private void Update()
    {

        Check();
        //if we press the mouse button
        if(Input.GetMouseButtonDown(1))
        {
            Interact();
        }    
    }

    private void Check()
    {
        Vector2 position = rgbd2D.position + characterController.LastMotionVector * OffSetDistance;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, SizeOfInteractableArea);

        foreach (Collider2D item in colliders)
        {
            Interactable hit = item.GetComponent<Interactable>();
            if(hit != null)
            {
                return;
            }
        }

    }

    public void Interact()
    {
        Vector2 position = rgbd2D.position + characterController.LastMotionVector * OffSetDistance;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, SizeOfInteractableArea);

        foreach (Collider2D item in colliders)
        {
            Interactable hit = item.GetComponent<Interactable>();
            if(hit != null)
            {
                hit.Interact(character);
                break;
            }
        }
    } 

}
