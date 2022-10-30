using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] string nameScessentialScene;
    [SerializeField] string nameNewGameStartScene;

    public void StartNewGame(){
        SceneManager.LoadScene(nameNewGameStartScene, LoadSceneMode.Single);
        SceneManager.LoadScene(nameScessentialScene, LoadSceneMode.Additive);
    }

    public void ExitGame()
    {
        Debug.Log("exit the game");
        Application.Quit();
    }
}
