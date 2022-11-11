using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FishGameCharacter : MonoBehaviour
{

    DisableControls disable;
    public bool startGame = false;
    [SerializeField] string FishScene;

    void Start()
    {
        disable = GetComponent<DisableControls>();
        
    }

    public void StartGame()
    {
        disable.DisableControl();
        startGame = true;
        SceneManager.LoadScene(FishScene, LoadSceneMode.Additive);
        
    }

}
