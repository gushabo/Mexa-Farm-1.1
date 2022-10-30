using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script goes in the game manager the go to the script CharacterInteractController and uncomment all the highlight things
public class HighLightController : MonoBehaviour
{
    //this is to put an arrow above the objects that we r going to interact
    [SerializeField] GameObject highlight;
    GameObject CurrentTarget;

    public void HighLight(GameObject target)
    {
        if(CurrentTarget == target)
        {
            return;
        }
        
        Vector3 position = target.transform.position + Vector3.up * 0.7f;
        HighLight(position);

    }

    public void HighLight(Vector3 position)
    {
        highlight.SetActive(true);
        highlight.transform.position = position;
    }

    public void Hide()
    {
        CurrentTarget = null;
        highlight.SetActive(false);
    }
}
