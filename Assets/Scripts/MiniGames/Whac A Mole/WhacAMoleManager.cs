using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    private float startingTime = 30f;
    //Global variables
    private float timeRemaining;
    private HashSet<mole> currentMoles = new HashSet<mole>();
    private int score;
    private bool playing = false;

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
        //Start with 30 seconds
        timeRemaining = startingTime;
        score = 0;
        scoreText.text = "0";
        playing = true;

    }

    public void GameOver()
    {
        //show the message
        gameOverText.SetActive(true);

        //Hide all moles
        foreach (mole mole in moles)
        {
            mole.StopGame();
        }
        //stop the game and show the UI
        playing = false;

    }

    private void Update()
    {
        if (playing)
        {
            //Updating time
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0 || lifes == 0)
            {
                timeRemaining = 0;
                GameOver();
            }
            timeText.text = $"{(int)timeRemaining / 60}:{(int)timeRemaining % 60:D2}";
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
    }

    public void AddScore(int moleIndex)
    {
        //add plus 1 in score
        score += 1;
        scoreText.text = $"{score}";
        //remove the mole from the actives
        currentMoles.Remove(moles[moleIndex]);

    }

    public void Missed(int moleIndex)
    {
        currentMoles.Remove(moles[moleIndex]);
        lifes--;
    }


}
