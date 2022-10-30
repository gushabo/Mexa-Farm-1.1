using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{
    [SerializeField] string nameMainScene;

    public void GameOver()
    {
        SceneManager.LoadScene(nameMainScene, LoadSceneMode.Single);
    }
}
