using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] Text targetText;
    [SerializeField] Text nameText;
    [SerializeField] Image portrait;
    
    DialogueContainer currentDialogue;
    int currentTextLine;

    [Range(0f, 1f)]
    [SerializeField] float visibleTextPercert;
    [SerializeField] float timePerLetter = 0.05f;
    float totalTimeToType, currentTime;
    string lineToShow;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PushText();
        }
        TypeOutText();
    }

    //to know if the text has finish and how much time we have to finish the text
    private void TypeOutText()
    {
        if (visibleTextPercert >= 1f) { return; }
        currentTime += Time.deltaTime;
        visibleTextPercert = currentTime / totalTimeToType;
        visibleTextPercert = Math.Clamp(visibleTextPercert, 0, 1f);
        UpdateText();
    }

    void UpdateText()
    {
        int letterCount = (int)(lineToShow.Length * visibleTextPercert);
        targetText.text = lineToShow.Substring(0, letterCount);
    }

    public void Initialize(DialogueContainer dialogueContainer)
    {
        Show(true);
        currentDialogue = dialogueContainer;
        currentTextLine = 0;
        CycleLine();
        UpdatePortrait();
    }

    //this gets the name and the portrait of the npc 
    private void UpdatePortrait()
    {
        portrait.sprite = currentDialogue.actor.portrait;
        nameText.text = currentDialogue.actor.Name;  
    }

    //this makes that the text moves foward
    private void PushText()
    {
        if(visibleTextPercert < 1f)
        {
            visibleTextPercert = 1f;
            UpdateText();
            return;
        }

        if (currentTextLine >= currentDialogue.line.Count)
        {
            Conclude();
        }
        else
        {
            CycleLine();
        }
    }

    void CycleLine()
    {
        lineToShow = currentDialogue.line[currentTextLine];
        totalTimeToType = lineToShow.Length * timePerLetter;
        currentTime = 0f;
        visibleTextPercert = 0f;
        targetText.text = "";

        currentTextLine += 1;
    }

    private void Conclude()
    {
        Show(false);
    }

   //this shows the text
    private void Show(bool v)
    {
        gameObject.SetActive(v);
    }
}
