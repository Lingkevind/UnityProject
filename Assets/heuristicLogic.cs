using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static aStarSearch;

public class heuristicLogic : MonoBehaviour
{
    public GameObject board;
    private float countDownTimer;
    private float elapsedTime;

    public GameObject countDownScene;
    public GameObject searchScene;

    public Text countDown;
    public Text timeElapsed;
    public Text searchCount;
    public Text depth;

    void Start()
    {
        resetTimer();
    }


    void Update()
    {
        Game game = board.GetComponent<Game>();
        if (game.getCurrentState() != "heuristic")
        {
            resetTimer();
            resetElapsed();
        }

        if (game.getCurrentState() == "heuristic")
        {
            if (countDownTimer >= 0)
            {
                countDownScene.SetActive(true);
                searchScene.SetActive(false);
                setCountDown();
                countDownTimer -= Time.deltaTime;
            }
            else
            {
                elapsedTime += Time.deltaTime;
                countDownScene.SetActive(false);
                searchScene.SetActive(true);
                setSearch(game.run.getCount());
                setDepth(game.run.getDepth());
                setTime();
            }
        }
    }

    public void setCountDown() {
        int t = (int)Math.Round(countDownTimer);
        countDown.text = "The game will begin imformedly search for the solution in: "+t+" s";
    }

    public void setSearch(int i) {
        searchCount.text=i+" searches processed";
    }

    public void setDepth(int i) {
        depth.text = "Depth: "+i;
    }

    public void resetTimer()
    {
        countDownTimer = 3f;
    }

    public void resetElapsed() {
        elapsedTime = 0f;
    }

    public void setTime()
    {
        int t = (int)Math.Round(elapsedTime);
        string h = "";
        string m = "";
        int hour = t / 3600;
        if (hour != 0)
        {
            h = hour.ToString() + "h ";
        }
        int minute = (t % 3600) / 60;
        if (minute != 0)
        {
            m = minute.ToString() + "m ";
        }
        int second = (t % 3600) % 60;
        timeElapsed.text = "Time elapsed: " + h + m + second + "s";
    }
}
