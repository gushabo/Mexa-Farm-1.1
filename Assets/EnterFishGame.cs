using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterFishGame : Interactable
{
    [SerializeField] string fishingSceneName;

    public override void Interact(Character character)
    { 
        SceneManager.LoadScene(fishingSceneName, LoadSceneMode.Single);
    }
}
