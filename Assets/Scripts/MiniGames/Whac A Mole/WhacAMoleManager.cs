using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WhacAMoleManager : MonoBehaviour
{
    [Header("settings")]
    public int lifes = 3;

    [SerializeField] public List<mole> moles;

    [Header("UI")]
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI finalText;
    public TextMeshProUGUI lifesText;

    //Global variables
    private HashSet<mole> currentMoles = new HashSet<mole>();
    private int score;
    private bool playing = false;
    //get the player
    GameObject go;
    //timer to get out the game
    float timer = 1.4f;
    private bool end;

    public void Start()
    {
        go = GameObject.Find("MainCharacter");
    }


    public void StartGame()
    {
        //Hide all the UI
        button.SetActive(false);
        gameOverText.SetActive(false);
        gameUI.SetActive(true);
        //Hide all the moles
        for (int i = 0; i < moles.Count; i++)
        {
            moles[i].Hide();
            moles[i].SetIndex(i);
        }
        //Remove any older game state
        currentMoles.Clear();
        score = 0;
        scoreText.text = "0";
        playing = true;

    }

    public void GameOver()
    {
        //show the message
        finalText.text = "U lose :c";
        gameOverText.SetActive(true);


        //Hide all moles
        foreach (mole mole in moles)
        {
            mole.StopGame();
        }
        //stop the game and show the UI
        playing = false;
        Debug.Log("perdiste pedazo de boludo");
        end = true;
        go.GetComponent<UsingArcade>().FinishGame(true);

    }

    private void Update()
    {
        lifesText.text = lifes.ToString();
        if (playing)
        {

            if (lifes == 0)
            {
                GameOver();
            }
            // Check if we need to start any more moles.
            if (lifes > 0)
            {
                // Choose a random mole.
                int index = Random.Range(0, moles.Count);
                // Doesn't matter if it's already doing something, we'll just try again next frame.
                if (!currentMoles.Contains(moles[index]))
                {
                    currentMoles.Add(moles[index]);
                    moles[index].Activate();
                }
            }

        }
        if(end)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
               SceneManager.UnloadScene("Whac a Mole");
            }
        }

    }


    public void AddScore(int moleIndex)
    {
        //add plus 1 in score
        score += 1;
        scoreText.text = $"{score}";
        //remove the mole from the actives
        currentMoles.Remove(moles[moleIndex]);
        if (score == 5)
        {
            uWIN();
        }

    }

    private void uWIN()
    {
        //show the message
        finalText.text = "U WIN!!!";
        gameOverText.SetActive(true);

        //Hide all moles
        foreach (mole mole in moles)
        {
            mole.StopGame();
        }
        //stop the game and show the UI
        playing = false;
        Debug.Log("ganaste gil");
        end = true;
        go.GetComponent<Currency>().Add(15);
        go.GetComponent<UsingArcade>().FinishGame(true);

    }

    public void Missed(int moleIndex)
    {
        currentMoles.Remove(moles[moleIndex]);
        lifes--;
    }


}
