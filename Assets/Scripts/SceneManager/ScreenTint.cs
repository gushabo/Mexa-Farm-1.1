using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTint : MonoBehaviour
{
    [SerializeField] Color unTintedColor;
    [SerializeField] Color TintedColor;
    public float speed = 0.5f;
    float f;

    Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    //this tint the scene
    public void Tint()
    {
        StopAllCoroutines();
        f = 0f;
        StartCoroutine(TintScreen());
    }

    public void UnTint()
    {
        StopAllCoroutines();
        f = 0f;
        StartCoroutine(UnTintScreen());
    }

    //this is to put the velocity of the tinting the scene
    private IEnumerator TintScreen()
    {
        while (f < 1f)
        {

            f += Time.deltaTime * speed;
            f = Math.Clamp(f, 0, 1f);

            Color c = image.color;
            c = Color.Lerp(unTintedColor, TintedColor, f);
            image.color = c;

            yield return new WaitForEndOfFrame();
        }

    }

    //this is to put the velocity of the Untinting the scene
    private IEnumerator UnTintScreen()
    {
        while (f < 1f)
        {

            f += Time.deltaTime * speed;
            f = Math.Clamp(f, 0, 1f);

            Color c = image.color;
            c = Color.Lerp(TintedColor, unTintedColor, f);
            image.color = c;

            yield return new WaitForEndOfFrame();
        }

    }
}
