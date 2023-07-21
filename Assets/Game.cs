using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.XR;
using static aStarSearch;

public static class global
{
    public static int IndexOf(int[] lis, int x)
    {
        for (int i = 0; i < lis.Length; i++)
        {
            if (x == lis[i])
            {
                return i;
            }
        }
        return -1;
    }

}
public class Board 
{
    public int[] list;
    public string lastMove = "None";
    public int stepCount = 0;
    public Board head;
    public Board nextStep;
    public Board previousStep;

    public Board() 
    {
        list =new int[]{1,2,3,4,5,6,7,8,9};
        lastMove = "None";
        stepCount = 0;
        previousStep = null;
    }

    public Board(int[] lis, string move, int count, Board previous)
    {
        list = lis;
        lastMove = move;
        stepCount = count;
        previousStep = previous;
    } 

    public Board(Board b) {
        list = (int[])b.list.Clone();
        lastMove= b.lastMove; 
        stepCount = b.stepCount;
        head = b.head;
        previousStep= b.previousStep;
        nextStep = b.nextStep;
    }

    public bool isGoal()
    {
        int[] Goal = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        for (int i = 0; i < 9; i++)
        {
            if (list[i] != Goal[i])
            {
                return false;
            }
        }
        return true;
    }

    
    public bool canLeft()
        {
            int i = global.IndexOf(list, 9);
            if (i == 0 || i == 3 || i == 6)
            {
                return false;
            }
            return true;
        }

        public bool canRight()
        {
            int i = global.IndexOf(list, 9);
            if (i == 2 || i == 5 || i == 8)
            {
                return false;
            }
            return true;
        }

        public bool canUp()
        {
            int i = global.IndexOf(list, 9);
            if (i >= 0 && i <= 2)
            {
                return false;
            }
            return true;
        }

        public bool canDown()
        {
            int i = global.IndexOf(list, 9);
            if (i >= 6 && i <= 8)
            {
                return false;
            }
            return true;
        }

    public int calManhattan()
    {
        int value = 0;
        int j = 0;
        for (int i = 0; i < list.Length; i++)
        {
            switch (i)
            {
                case 0:
                    j = list[i];
                    if (j >= 4 && j <= 6)
                    {
                        value += 1;
                    }
                    if (j >= 7 && j <= 9)
                    {
                        value += 2;
                    }
                    if (new int[] { 2, 5, 8 }.Contains(j))
                    {
                        value += 1;
                    }
                    if (new int[] { 3, 6, 9 }.Contains(j))
                    {
                        value += 2;
                    }
                    break;
                case 1:
                    j = list[i];
                    if (j >= 4 && j <= 6)
                    {
                        value += 1;
                    }
                    if (j >= 7 && j <= 9)
                    {
                        value += 2;
                    }
                    if (new int[] { 1, 3, 4, 6, 7, 9 }.Contains(j))
                    {
                        value += 1;
                    }
                    break;
                case 2:
                    j = list[i];
                    if (j >= 4 && j <= 6)
                    {
                        value += 1;
                    }
                    if (j >= 7 && j <= 9)
                    {
                        value += 2;
                    }
                    if (new int[] { 2, 5, 8 }.Contains(j))
                    {
                        value += 1;
                    }
                    if (new int[] { 1, 4, 7 }.Contains(j))
                    {
                        value += 2;
                    }
                    break;
                case 3:
                    j = list[i];
                    if ((j >= 1 && j <= 3) || (j >= 7 && j <= 9))
                    {
                        value += 1;
                    }
                    if (new int[] { 2, 5, 8 }.Contains(j))
                    {
                        value += 1;
                    }
                    if (new int[] { 3, 6, 9 }.Contains(j))
                    {
                        value += 2;
                    }
                    break;
                case 4:
                    j = list[i];
                    if (new int[] { 2, 4, 6, 8 }.Contains(j))
                    {
                        value += 1;
                    }
                    if (new int[] { 1, 3, 5, 7, 9 }.Contains(j))
                    {
                        value += 2;
                    }
                    break;
                case 5:
                    j = list[i];
                    if ((j >= 1 && j <= 3) || (j >= 7 && j <= 9))
                    {
                        value += 1;
                    }
                    if (new int[] { 2, 5, 8 }.Contains(j))
                    {
                        value += 1;
                    }
                    if (new int[] { 1, 4, 7 }.Contains(j))
                    {
                        value += 2;
                    }
                    break;
                case 6:
                    j = list[i];
                    if (j >= 4 && j <= 6)
                    {
                        value += 1;
                    }
                    if (j >= 1 && j <= 3)
                    {
                        value += 2;
                    }
                    if (new int[] { 2, 5, 8 }.Contains(j))
                    {
                        value += 1;
                    }
                    if (new int[] { 3, 6, 9 }.Contains(j))
                    {
                        value += 2;
                    }
                    break;
                case 8:
                    j = list[i];
                    if (j >= 4 && j <= 6)
                    {
                        value += 1;
                    }
                    if (j >= 1 && j <= 3)
                    {
                        value += 2;
                    }
                    if (new int[] { 1, 3, 4, 6, 7, 9 }.Contains(j))
                    {
                        value += 1;
                    }
                    break;
                default:
                    break;
            }
        }
        return value;
    }

    public Board(heuristicBoard hb) {
        list = hb.list;
        lastMove = hb.lastMove;
        stepCount = hb.stepCount;
        head = hb.head;
        previousStep = hb.previousStep;
        nextStep = hb.nextStep;
    }
}

public class heuristicBoard : Board {

    public heuristicBoard leftBranch;
    public heuristicBoard rightBranch;
    public heuristicBoard upBranch;
    public heuristicBoard downBranch;
    private int manhattanValue;

    public heuristicBoard(int[] lis, string move, int count) {
        list= lis;
        lastMove = move;
        stepCount = count;
        previousStep = null;
        setManhattan();
    }

    public heuristicBoard(heuristicBoard hb) {
        list = hb.list;
        lastMove = hb.lastMove;
        stepCount = hb.stepCount;
        head = hb.head;
        previousStep = hb.previousStep;
        nextStep = hb.nextStep;
        leftBranch= hb.leftBranch;
        rightBranch= hb.rightBranch;
        upBranch= hb.upBranch;
        downBranch= hb.downBranch;
        manhattanValue = hb.getManhattan();
    }

    public heuristicBoard(Board b):base(b) {
        list = b.list;
        lastMove = null;
        stepCount = 0;
        head = this;
        previousStep = null;
        nextStep = null;
        leftBranch = null;
        rightBranch = null;
        upBranch = null;
        downBranch = null;
        setManhattan();
    }

    public void setManhattan() {
        manhattanValue=calManhattan();
    }

    public int getManhattan()
    {
        return manhattanValue;
    }

    public int[] swap (int i, int j)
    {
        int[] lis = (int[])list.Clone();
        int k = lis[i];
        lis[i] = lis[j];
        lis[j] = k;
        return lis;
    }

    public void genLeft() {
            int i = global.IndexOf(list,9);
            int[] lis = swap(i, i - 1);
            leftBranch = new heuristicBoard(lis,"left",this.stepCount+1);
            leftBranch.previousStep = this;
    }

    public void genRight()
    {
        
            int i = global.IndexOf(list, 9);
            int[] lis = swap(i, i + 1);
            rightBranch = new heuristicBoard(lis, "right", this.stepCount + 1);
            rightBranch.previousStep = this;
    }

    public void genUp()
    {
            int i = global.IndexOf(list, 9);
            int[] lis = swap(i, i -3);
            upBranch = new heuristicBoard(lis, "Up", this.stepCount + 1);
            upBranch.previousStep = this;
    }

    public void genDown()
    {
            int i = global.IndexOf(list, 9);
            int[] lis = swap(i, i + 3);
            downBranch = new heuristicBoard(lis, "Down", this.stepCount + 1);
            downBranch.previousStep = this;
    }

    public void genSuccessor() {
        if (base.canLeft())
        {
            genLeft();
        }
        if (base.canRight())
        {
            genRight();
        }
        if (base.canUp()) 
        {
            genUp();
        }
        if (base.canDown())
        {
            genDown();
        }
    }
}

public class aStarSearch
{
    heuristicBoard firstBoard;
    List<heuristicBoard> openList;
    List<heuristicBoard> closedList;
    int searchCount;
    int depth;

    public aStarSearch(heuristicBoard hb)
    {
        firstBoard = hb;
        openList = new List<heuristicBoard>();
        openList.Add(hb);
        closedList = new List<heuristicBoard>();
        searchCount = 0;
        depth = 0;
    }

    public bool repeated(int[] lis)
    {
        for (int i = 0; i < closedList.Count; i++)
        {
            if (closedList[i].list.SequenceEqual(lis))
            {
                return true;
            }
        }
        return false;
    }

    public void increaseCount()
    {
        searchCount++;
    }

    public int getCount() {
        return searchCount;
    }

    public int getDepth() {
        return depth;
    }

    public void appendSuccessors(heuristicBoard hb)
    {
        hb.genSuccessor();
        int[] lis = new int[] { };
        if (hb.leftBranch != null)
        {
            lis = hb.leftBranch.list;
            if (!repeated(lis))
            {
                openList.Add(hb.leftBranch);
            }
        }
        if (hb.rightBranch != null)
        {
            lis = hb.rightBranch.list;
            if (!repeated(lis))
            {
                openList.Add(hb.rightBranch);
            }
        }
        if (hb.upBranch != null)
        {
            lis = hb.upBranch.list;
            if (!repeated(lis))
            {
                openList.Add(hb.upBranch);
            }
        }
        if (hb.downBranch != null)
        {
            lis = hb.downBranch.list;
            if (!repeated(lis))
            {
                openList.Add(hb.downBranch);
            }
        }
    }

    public heuristicBoard search()
    { 
        if (openList.Count > 0)
        {
            List<int> valueList = new List<int>();
            for (int i = 0; i < openList.Count; i++)
            {
                valueList.Add(openList[i].getManhattan() + openList[i].stepCount);
            }
            int min = valueList.Min();
            int minPosition = valueList.IndexOf(min);
            heuristicBoard result = new heuristicBoard(openList[minPosition]);
            increaseCount();
            depth = result.stepCount;
            if (result.isGoal())
            {
                Debug.Log("Found the goal");
                return result;
            }
            else
            {
                Debug.Log("searching: " + openList[minPosition].list[0]+ openList[minPosition].list[1]+ openList[minPosition].list[2]+ openList[minPosition].list[3]+ openList[minPosition].list[4]+ openList[minPosition].list[5]+ openList[minPosition].list[6] + openList[minPosition].list[7] + openList[minPosition].list[8]);
                closedList.Add(openList[minPosition]);
                heuristicBoard copyBoard = new heuristicBoard(openList[minPosition]);
                openList.RemoveAt(minPosition);
                appendSuccessors(copyBoard);
                return result;
            }
        }
        return null;
    }
}

public abstract class State
{
    protected Game game;
    public string stateName;

    public abstract void enterState();
    public abstract void updateState();
    public abstract void exitState();

}

public class startState : State
{
    public startState(Game g)
    {
        game = g;
        stateName = "start";
    }

    public override void enterState()
    {
        Debug.Log("entering start state");
        game.HUD.SetActive(false);
        game.winScene.SetActive(false);
        game.heuristicScene.SetActive(false);
        game.finalScene.SetActive(false);
        game.startScene.SetActive(true);
        game.buttonClicked = false;
        game.board = new Board();

        game.genRandom(game.board);

        game.headBoard = new Board(game.board);
        game.genBoard(game.board);

    }

    public override void updateState()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if(game.board.canLeft())
            {
                game.board = game.Left();
                game.transferState("play");
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (game.board.canRight())
            {
                game.board = game.Right();
                game.transferState("play");
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (game.board.canUp())
            {
                game.board = game.Up();
                game.transferState("play");
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (game.board.canDown())
            {
                game.board = game.Down();
                game.transferState("play");
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            game.board = new Board();
            game.genRandom(game.board);
            game.genBoard(game.board);
        }
    }

    public override void exitState()
    {
        Debug.Log("exiting start state");
        game.startScene.SetActive(false);
    }
}

public class playState : State
{ 
    public playState(Game g)
    {
        game = g;
        stateName = "play";
    }

    public override void enterState()
    {
        Debug.Log("entering play state");
        game.HUD.SetActive(true);
        game.heuristicScene.SetActive(false);
        game.genBoard(game.board);
    }

    public override void updateState()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            game.board = game.Left();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            game.board = game.Right();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            game.board = game.Up();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            game.board = game.Down();
        }
        if (Input.GetKeyDown(KeyCode.R))
        { 
            game.transferState("start");
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            game.undo(); 
        }
        if (game.board.isGoal())
        {
            game.transferState("win");
        }
    }

    public override void exitState()
    {
        Debug.Log("exiting play state");
    }
}

public class heuristicState : State {

    float timer;
    public heuristicState(Game g)
    {
        game = g;
        stateName = "heuristic";
        timer = 3f;
        game.hBoard = new heuristicBoard(game.board);
        game.copyBoard = game.board;
        game.run = new aStarSearch(game.hBoard);
    }

    public override void enterState()
    {
        Debug.Log("entering heuristic state");
        game.heuristicScene.SetActive(true);
        game.HUD.SetActive(false);
    }

    public override void updateState()
    {
        if (timer>=0) {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            Board searchedBoard = new Board(game.run.search());
            game.genBoard(searchedBoard);
            if (searchedBoard.isGoal())
            {
                game.board = searchedBoard;
                game.transferState("donesearch");
            }
        }
    }

    public override void exitState()
    {
        game.heuristicScene.SetActive(false);
        Debug.Log("exiting heuristic state");
    }
}

public class doneSearchState:State {
    List<Board> Boards;
    float timer;

    public doneSearchState(Game g) {
        game = g;
        stateName = "donesearch";
        timer = 1f;
        Boards = new List<Board>();
    }
    

    public override void enterState() {
        Debug.Log("entering done search state");
        Board copyboard = new Board(game.board);
        Boards.Add(copyboard);
        for(int i=0;i<game.run.getDepth();i++) {
            if (copyboard.previousStep!=null) {
                Boards.Add(copyboard.previousStep);
                copyboard = copyboard.previousStep;
            }
        }
        game.finalScene.SetActive(true);

    }

    public override void updateState()
    {
        if (game.buttonClicked)
        {
            game.finalScene.SetActive(false);
            if (Boards.Count > 0)
            {
                if (timer >= 0)
                {
                    timer -= Time.deltaTime;
                }
                else 
                { 
                    game.board = Boards[Boards.Count - 1];
                    game.genBoard(game.board);
                    Boards.RemoveAt(Boards.Count-1);
                    timer = 1f;
                }
            }
            else
            {
                game.transferState("win");
            }
        }
    }

    public override void exitState()
    {
        Debug.Log("Exiting done search state");
    }
}

public class winState : State
{

    public winState(Game g)
    {
        game = g;
        stateName = "win";
    }

    public override void enterState()
    {
        Debug.Log("entering win state");
        game.winScene.SetActive(true);
    }

    public override void updateState()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            game.transferState("start");  
        }
    }

    public override void exitState()
    {
        Debug.Log("exiting win state");
        game.winScene.SetActive(false);
    }
}



public class Game : MonoBehaviour
{
    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;
    public GameObject five;
    public GameObject six;
    public GameObject seven;
    public GameObject eight;
    public GameObject blank;

    public Board board;
    public Board headBoard;
    public Board copyBoard;
    public heuristicBoard hBoard;
    public aStarSearch run;
    private State currentState;

    public GameObject startScene;
    public GameObject HUD;
    public GameObject winScene;
    public GameObject heuristicScene;
    public GameObject finalScene;

    public Button playButton;
    public bool buttonClicked = false;

    void Start()
    {
        transferState("start");
    }

    void Update()
    {
        currentState?.updateState();
    }

    public void transferState(string stateName)
    {
        currentState?.exitState();
        if (stateName == "start")
        {
            currentState = new startState(this);
        }
        if (stateName == "play")
        {
            currentState = new playState(this);
        }
        if (stateName == "heuristic")
        {
            currentState = new heuristicState(this);
        }
        if (stateName == "donesearch") {
            currentState = new doneSearchState(this);
        }
        if (stateName == "win")
        {
            currentState = new winState(this);
        }
        currentState?.enterState();
    }

    public string getCurrentState() {
        return currentState.stateName;
    }

    public void Replay()
    {
        transferState("start");
    }

    public void ToPlay() {
        transferState("play");
    }

    public void toSearch() {
        transferState("heuristic");
    }

    public void clickPlay() {
        if (buttonClicked==false) {
            buttonClicked  = true;
        }
    }

    public void setHead(Board b) {
        b.head = headBoard;
    }

    public void undo() {
        if (board.previousStep != null)
        {
            board = board.previousStep;
        }
        genBoard(board);
    }

    public void swap(Board b,int i, int j)
    {
        int k = b.list[i];
        b.list[i] = b.list[j];
        b.list[j] = k;
    }

    public GameObject convertInt(int i)
    {
        switch (i)
        {
            case 1:
                return one;
                break;
            case 2:
                return two;
                break;
            case 3:
                return three;
                break;
            case 4:
                return four;
                break;
            case 5:
                return five;
                break;
            case 6:
                return six;
                break;
            case 7:
                return seven;
                break;
            case 8:
                return eight;
                break;
            case 9: return blank;
            default: return null;
        }
    }

    public void genRandom(Board b)
    {
        int randomNumber = Random.Range(30,50);
        for (int i = 0; i < randomNumber; i++)
        {
            bool moved = false;
            while (moved == false)
            {int randomDirection = Random.Range(1, 5);
                if (randomDirection == 1) {
                    if (b.canLeft()) {
                        int j = global.IndexOf(b.list, 9);
                        swap(b, j,j-1);
                        moved = true;
                    }
                }
                if (randomDirection == 2) {
                    if (b.canRight())
                    {
                        int j = global.IndexOf(b.list, 9);
                        swap(b, j, j + 1);
                        moved = true;
                    }
                }
                if (randomDirection==3)
                {
                    if (b.canUp())
                    {
                        int j = global.IndexOf(b.list, 9);
                        swap(b, j, j - 3);
                        moved = true;
                    }
                }
                if (randomDirection == 4)
                {
                    if (b.canDown())
                    {
                        int j = global.IndexOf(b.list, 9);
                        swap(b, j, j + 3);
                        moved = true;
                    }
                }
            }
        }
    }

    public void genBoard(Board b)
    {
        convertInt(b.list[0]).transform.position = new Vector3(-7.67f, 2.28f, -1);
        convertInt(b.list[1]).transform.position = new Vector3(-5.42f, 2.28f, -1);
        convertInt(b.list[2]).transform.position = new Vector3(-3.17f, 2.28f, -1);
        convertInt(b.list[3]).transform.position = new Vector3(-7.67f, 0.03f, -1);
        convertInt(b.list[4]).transform.position = new Vector3(-5.42f, 0.03f, -1);
        convertInt(b.list[5]).transform.position = new Vector3(-3.17f, 0.03f, -1);
        convertInt(b.list[6]).transform.position = new Vector3(-7.67f, -2.22f, -1);
        convertInt(b.list[7]).transform.position = new Vector3(-5.42f, -2.22f, -1);
        convertInt(b.list[8]).transform.position = new Vector3(-3.17f, -2.22f, -1);
    }

    public Board genNewBoard(Board b, string m) {
        b.lastMove = m;
        b.stepCount = board.stepCount + 1;
        b.head = board.head;
        b.previousStep = board;
        setHead(b);
        return b;
    } 


    public Board Left()
    {
        if (board.canLeft())
        {
            Board newBoard = new Board(board);
            int i = global.IndexOf(newBoard.list, 9);
            moveRight(newBoard.list[i - 1]);
            moveLeft(9);
            swap(newBoard, i, i - 1);
            return genNewBoard(newBoard, "Left");
        }
        else
        { return board; }
    }

    public Board Right()
    {
        if (board.canRight())
        {
            Board newBoard = new Board(board);
            int i = global.IndexOf(newBoard.list, 9);
            moveLeft(newBoard.list[i + 1]);
            moveRight(9);
            swap(newBoard,i, i + 1);
            return genNewBoard(newBoard, "Right");
        }
        else
        { return board; }
    }

    public Board Up()
    {
        if (board.canUp())
        {
            Board newBoard = new Board(board);
            int i = global.IndexOf(newBoard.list, 9);
            moveDown(newBoard.list[i -3]);
            moveUp(9);
            swap(newBoard, i, i -3);
            return genNewBoard(newBoard,"Up");
        }
        else
        { return board; }
    }

    public Board Down()
    {
        if (board.canDown())
        {
            Board newBoard = new Board(board);
            int i = global.IndexOf(newBoard.list, 9);
            moveUp(newBoard.list[i + 3]);
            moveDown(9);
            swap(newBoard,i, i + 3);
            return genNewBoard(newBoard,"Down");
        }
        else
        { return board; }
    }

    void moveLeft(int i)
    {
        moveSet move = convertInt(i).GetComponent<moveSet>();
        move.moveLeft();
    }

    void moveRight(int i)
    {
        moveSet move = convertInt(i).GetComponent<moveSet>();
        move.moveRight();
    }

    void moveUp(int i)
    {
        moveSet move = convertInt(i).GetComponent<moveSet>();
        move.moveUp();
    }

    void moveDown(int i)
    {
        moveSet move = convertInt(i).GetComponent<moveSet>();
        move.moveDown();
    }

    [ContextMenu("Win the game")]
    void makeWin() {
        board.list =new int[]{ 1,2,3,4,5,6,7,8,9};
        genBoard(board);
    }

    public void exitGame() {
        Application.Quit();
    }
}
