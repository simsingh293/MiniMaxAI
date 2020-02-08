using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public GameObject nodePrefab;
    public Material X_Material;
    public Material O_Material;
    public Material None_Material;
    public List<boardNode> boardNodes = new List<boardNode>();
    public List<GameObject> nodeObjs = new List<GameObject>();

    public List<(int, int)> nodePositions = new List<(int, int)>();

    Gameplay gameplay;
    public Debugger debugger;

    public bool gameStarted = false;
    public bool gameOver = false;
    public bool gameWon = false;
    public float resetTimer = 0;

    public int p1Moves = 0;
    public int p2Moves = 0;


    public enum PlayerType
    {
        PlayerOne,
        PlayerTwo,
        AI_One,
        AI_Two
    }

    public struct Player
    {
        public PlayerType playerType;
        public boardNode.b_DesignationType designationType;
        public Material material;
        public int moves;
        public int next;
    }

    public PlayerType P1;
    public PlayerType P2;

    public Player currentPlayer;

    public Player[] players = new Player[2];

    // Start is called before the first frame update
    void Start()
    {
        GenerateBoard();

        gameplay = Gameplay.singleton;
        debugger = gameObject.AddComponent<Debugger>();

        players[0].playerType = P1;
        players[0].designationType = boardNode.b_DesignationType.X;
        players[0].material = X_Material;
        players[0].next = 1;
        
        players[1].playerType = P2;
        players[1].designationType = boardNode.b_DesignationType.O;
        players[1].material = O_Material;
        players[1].next = 0;

        currentPlayer = players[0];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckForInteraction();
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 10))
            {
                for (int i = 0; i < nodeObjs.Count; i++)
                {
                    if (hit.collider.gameObject == nodeObjs[i])
                    {
                        boardNode node = boardNodes[i];

                        debugger.currentlySelectedDesignation = node.b_CurrentDesignation.ToString();
                    }

                }
            }
        }


        gameOver = CheckForFullBoard();

        if(p1Moves >= 3 || p2Moves >= 3)
        {
            gameWon = CheckForWin();
        }

        if (gameOver || gameWon && gameStarted)
        {
            resetTimer += Time.deltaTime;

            if(resetTimer > 2)
            {
                ResetBoard();
            }
        }
    }


    

    void GenerateBoard()
    {
        int count = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                count++;

                GameObject temp = Instantiate(nodePrefab);

                temp.transform.parent = this.transform;

                Vector3 position = Vector3.zero;

                position.x = i * 3;
                position.z = j * 3;

                temp.transform.position = position;

                temp.name = "Node " + count;

                nodeObjs.Add(temp);

                boardNode node = temp.AddComponent<boardNode>();
                node.Init();

                boardNodes.Add(node);

                nodePositions.Add(node.b_pos);
            }
        }
    }

    void ResetBoard()
    {
        for (int i = 0; i < boardNodes.Count; i++)
        {
            boardNodes[i].b_CurrentDesignation = boardNode.b_DesignationType.none;
            boardNodes[i].b_CurrentMaterial = None_Material;
            boardNodes[i].ApplyMaterial();
        }

        gameStarted = false;
        gameWon = false;

        currentPlayer = players[0];
        resetTimer = 0;
        p1Moves = 0;
        p2Moves = 0;
    }


    void CheckForInteraction()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 10))
        {
            
            if (!gameStarted)
            {
                gameStarted = true;
            }

            for (int i = 0; i < nodeObjs.Count; i++)
            {

                if (hit.collider.gameObject == nodeObjs[i])
                {
                    boardNode node = boardNodes[i];

                    //Debug.Log(node.b_pos + " : " + node.b_CurrentDesignation);
                    //Debug.Log(gameplay.CheckNodeAvailability(node));

                    if (gameplay.CheckNodeAvailability(boardNodes[i]))
                    {
                        if(currentPlayer.next == 1)
                        {
                            p1Moves++;
                        }
                        else if(currentPlayer.next == 0)
                        {
                            p2Moves++;
                        }
                        gameplay.SetNode(boardNodes[i], currentPlayer, this);
                    }
                }

            }
        }
        Debug.DrawLine(ray.origin, ray.origin + (ray.direction * 10));
    }


    bool CheckForFullBoard()
    {
        bool result = true;


        for (int i = 0; i < boardNodes.Count; i++)
        {
            if(boardNodes[i].b_CurrentDesignation == boardNode.b_DesignationType.none)
            {
                result = false;
                break;
            }
        }


        return result;
    }


    bool CheckForWin()
    {
        bool result = false;

        List<int> tempList = new List<int>();

        

        // HORIZONTAL
        for (int i = 1; i <= 3; i++)
        {
            for (int j = 0; j < boardNodes.Count; j++)
            {
                if(boardNodes[j].b_pos.Item1 == i)
                {
                    Debug.Log(boardNodes[j].b_pos);
                    tempList.Add(j);
                }
            }

            if(boardNodes[tempList[0]].b_CurrentDesignation == boardNodes[tempList[1]].b_CurrentDesignation 
                &&
                boardNodes[tempList[1]].b_CurrentDesignation == boardNodes[tempList[2]].b_CurrentDesignation 
                &&
                boardNodes[tempList[2]].b_CurrentDesignation == boardNodes[tempList[0]].b_CurrentDesignation)
            {
                Debug.Log(tempList[0] + " " + tempList[1] + " " + tempList[2]);
                Debug.Log(boardNodes[tempList[0]].b_CurrentDesignation + " " + boardNodes[tempList[1]].b_CurrentDesignation + " " + boardNodes[tempList[2]].b_CurrentDesignation);
                return true;
            }
            else
            {
                tempList.Clear();
        }
        

        }


        // VERTICAL
        //for (int k = 1; k <= 3; k++)
        //{
        //    for (int l = 0; l < boardNodes.Count; l++)
        //    {
        //        if (nodePositions[l].Item2 == k)
        //        {
        //            tempList.Add(l);
        //        }
        //    }

        //    if (boardNodes[tempList[0]].b_CurrentDesignation == boardNodes[tempList[1]].b_CurrentDesignation
        //        &&
        //        boardNodes[tempList[1]].b_CurrentDesignation == boardNodes[tempList[2]].b_CurrentDesignation
        //        &&
        //        boardNodes[tempList[2]].b_CurrentDesignation == boardNodes[tempList[0]].b_CurrentDesignation)
        //    {
        //        Debug.Log(tempList.Count);
        //        return true;
        //    }
            

        //    tempList.Clear();
        //}


        tempList.Clear();




        return result;
    }
}
