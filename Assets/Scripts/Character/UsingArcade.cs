using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UsingArcade : MonoBehaviour
{

    [SerializeField] GameObject arcade;
    [SerializeField] string Minigame1;
    [SerializeField] string Minigame2;

    //all the UI's that i want to hide (the clock, the money, the toolbar, etc)
    [SerializeField] GameObject toolbar;
    [SerializeField] GameObject moneyNclockUI;
    [SerializeField] GameObject stamina;

    public void OpenMenu()
    {
        arcade.SetActive(true);
        toolbar.SetActive(false);
        moneyNclockUI.SetActive(false);
        stamina.SetActive(false);
    }

    public void CloseArcade()
    {
        arcade.SetActive(false);
        toolbar.SetActive(true);
        moneyNclockUI.SetActive(true);
        stamina.SetActive(true);
    }

    public void ChangeScene(bool scene)
    {
        if (scene)
        {
            SceneManager.LoadScene(Minigame1, LoadSceneMode.Additive);
            arcade.SetActive(false);
        }
        else
        {
            SceneManager.LoadScene(Minigame2, LoadSceneMode.Additive);
            arcade.SetActive(false);
        }
    }

    public void FinishGame(bool end)
    {
        if (end)
        {
            StartCoroutine(finish());
        }
    }

    IEnumerator finish()
    {
        yield return new WaitForSeconds(1.5f);
        arcade.SetActive(false);
        toolbar.SetActive(true);
        moneyNclockUI.SetActive(true);
        stamina.SetActive(true);
        gameObject.GetComponent<DisableControls>().EnableControl();
        StopAllCoroutines();
    }

}
