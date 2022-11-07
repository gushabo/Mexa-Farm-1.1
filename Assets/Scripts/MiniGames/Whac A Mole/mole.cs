using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mole : MonoBehaviour
{

    [Header("Graphics")]
    [SerializeField] private Sprite Mole;
    [SerializeField] private Sprite moleHit;

    [Header("Game Manager")]
    [SerializeField] private WhacAMoleManager manager;

    //the space of the sprite to show it and hide it
    private Vector2 startPosition = new Vector2(0f, -2.56f);
    private Vector2 endPosition = Vector2.zero;
    //the time that we are showing the mole
    private float showDuration = 0.7f;
    private float duration = 1;

    private SpriteRenderer renderer;

    //mole parameters
    private bool hitable = true;
    private int moleIndex = 0;


    public void SetIndex(int index)
    {
        moleIndex = index;
    }

    private void Awake()
    {
        //Get the references of the components;
        renderer = GetComponent<SpriteRenderer>();
    }

    private IEnumerator showHide(Vector2 start, Vector2 end)
    {
        //always start on the start :b
        transform.localPosition = start;

        //show the mole
        float elapsed = 0f;
        while (elapsed < showDuration)
        {
            transform.localPosition = Vector2.Lerp(start, end, elapsed / showDuration);
            //Update the max frameRate
            elapsed += Time.deltaTime;
            yield return null;
        }

        //Make sure we are at the end
        transform.localPosition = end;

        //Wait for the duration to pass
        yield return new WaitForSeconds(duration);

        //Hide the mole
        elapsed = 0f;
        while (elapsed < showDuration)
        {
            transform.localPosition = Vector2.Lerp(end, start, elapsed / showDuration);
            //Update the max frameRate
            elapsed += Time.deltaTime;
            yield return null;
        }

        //Make sure we are again in the start position
        transform.localPosition = start;

        //if we got to the end and it's still hitable it means we miss
        if (hitable)
        {
            hitable = false;
            manager.Missed(moleIndex);
        }


    }

    private IEnumerator QuickHide()
    {
        yield return new WaitForSeconds(0.5f);
        if (!hitable)
        {
            Hide();
        }
    }

    public void Hide()
    {
        //Set the mole parameters to hide it
        transform.localPosition = startPosition;
    }

    private void OnMouseDown()
    {
        if (hitable)
        {
            renderer.sprite = moleHit;
            //stop the animation
            StopAllCoroutines();
            StartCoroutine(QuickHide());
            //add score
            manager.AddScore(moleIndex);
            //Turn off the hitable on the mole
            hitable = false;
        }

    }

    public void Activate()
    {
        CreateNext();
        StartCoroutine(showHide(startPosition, endPosition));
    }

    private void CreateNext()
    {
        // Create a standard one.
        renderer.sprite = Mole;
        // Mark as hittable so we can register an onclick event.
        hitable = true;
    }



    public void StopGame()
    {
        hitable = false;
        StopAllCoroutines();
    }

}
