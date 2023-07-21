using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;

public class Logic : MonoBehaviour
{
    public GameObject Board;
    public Text time;
    public Text step;
    public Text move;
    public Text onebla;
    public Text twoStep;
    public Text twoMove;
    public Text congrats;

    [SerializeField]
    private float timer;
    private int stepCount;
    private string lastMove;

    public GameObject heuristic;
    public GameObject one;
    public GameObject two;
    public GameObject tutorial;

    private void Start()
    {
        setTime();
        setCount(0);
        setMove("None");
        heuristic.SetActive(false);
    }

    private void Update()
    {
        Game game = Board.GetComponent<Game>();
        if (game.getCurrentState() != "play") {
            heuristic.SetActive(false);
            tutorial.SetActive(false);
        }
        if (game.getCurrentState() == "play")
        {
            tutorial.SetActive(true);
            timer += Time.deltaTime;
            setTime();
            setCount(game.board.stepCount);
            setMove(game.board.lastMove);
            if (timer >= 5)
            {
                heuristic.SetActive(true);
            }
        }

        if (game.getCurrentState() == "start")
        {
            resetTimer();
        }

        if (game.getCurrentState() == "donesearch") {
            if (game.buttonClicked == false) {
                one.SetActive(true);
                setOne(game.run.getCount(), game.run.getDepth());
            }
            if (game.buttonClicked == true) {
                one.SetActive(false);
                setTwoCount(game.board.stepCount);
                if (game.board.lastMove == null)
                { setTwoMove("None");
                }
                else
                {
                    setTwoMove(game.board.lastMove);
                }
            }
        }
        if (game.buttonClicked == true)
        {
            two.SetActive(true);
        }
        else {
            two.SetActive(false);
        }

        if (game.getCurrentState() == "win") { 
            int difficulty = game.headBoard.calManhattan();
            setDifficulty(difficulty);
        }
    }

    public void resetTimer() {
        timer = 0;
    }

    public void setTime()
    {
        int t = (int)Math.Round(timer);
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
        time.text = "Time used: " + h + m + second + "s";
    }

    public void setCount(int i)
    {
        stepCount = i;
        step.text = "Steps used: " + stepCount.ToString();
    }

    public void setMove(string m)
    {
        lastMove = "Last move: " + m;
        move.text = lastMove;
    }

    public void setOne(int search, int depth) {
        onebla.text = "The game has found the goal in " + search + " tries with the depth of " + depth;
    }

    public void setTwoCount(int i)
    {
        stepCount = i;
        twoStep.text = "Steps used: " + stepCount.ToString();
    }

    public void setTwoMove(string m)
    {
        lastMove = "Last move: " + m;
        twoMove.text = lastMove;
    }

    public void setDifficulty(int i) {
        congrats.text = "Congratulations! the board you just solve has the difficulty level of "+i.ToString();
    }
}
