using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYS_Gameplay : MonoBehaviour
{
    public SYS_GameBoard m_GameBoard;


    public enum PlayerType
    {
        P1,
        P2,
        A1,
        A2
    }

    public PlayerType Player1 = PlayerType.P1;
    public PlayerType Player2 = PlayerType.P2;

    public PlayerType currentPlayerTurn;

    public int turnCount = 0;

    public bool isGameStarted = false;
    public bool isGameWon = false;

    Coroutine currentCo;

    private void Awake()
    {
        currentPlayerTurn = Player1;
    }


    private void Update()
    {
        //GameLoop();
    }


    void SetupGame()
    {
        if()
    }




    void GameLoop()
    {
        if(!isGameStarted && currentCo != null)
        {
            StopCoroutine(currentCo);
            currentCo = null;
        }

        if (!isGameWon)
        {
            if (Input.GetMouseButtonDown(0) && !m_GameBoard.isBoardFull)
            {
                CheckForInteraction();
            }
            else if(turnCount > 4 || m_GameBoard.isBoardFull)
            {
                isGameWon = m_GameBoard.CheckForWin();
            }

        }

        if (isGameWon)
        {
            Debug.Log("Winner is: " + m_GameBoard.winLine[0].GetNodeState().ToString());
        }
        
        if(isGameWon || m_GameBoard.isBoardFull)
        {
            if(currentCo == null)
            {
                currentCo = StartCoroutine(ResetTheGame());
            }
        }
    }

    


    void CheckForInteraction()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 15))
        {
            for (int i = 0; i < m_GameBoard.gridNodes.Count; i++)
            {
                if(hit.collider.gameObject == m_GameBoard.gridNodes[i])
                {
                    //Debug.Log("Hit a node");

                    //CheckAvailability(m_GameBoard.nodeObjs[i]);

                    if (CheckAvailability(m_GameBoard.nodeObjs[i]))
                    {
                        if (!isGameStarted)
                        {
                            isGameStarted = true;
                        }

                        SetNode(m_GameBoard.nodeObjs[i]);

                        IterateTurn();
                    }
                }
            }

        }
        Debug.DrawLine(ray.origin, ray.origin + (ray.direction * 10));
    }









    bool CheckAvailability(SYS_BoardNode node)
    {
        if(node.GetNodeState() != SYS_BoardNode.nodeState.none)
        {
            Debug.Log("Node is not available");
            return false;
        }
        else
        {
            Debug.Log("Node is available");
            return true;
        }
    }

    void SetNode(SYS_BoardNode selectedNode)
    {
        // p1 turn
        if(currentPlayerTurn == Player1)
        {
            selectedNode.SetNodeState(SYS_BoardNode.nodeState.X);
        }
        // p2 turn
        else if(currentPlayerTurn == Player2)
        {
            selectedNode.SetNodeState(SYS_BoardNode.nodeState.O);
        }

        selectedNode.ActivateSymbol();
    }

    void IterateTurn()
    {
        turnCount++;

        if(currentPlayerTurn == Player1)
        {
            currentPlayerTurn = Player2;
        }
        else if(currentPlayerTurn == Player2)
        {
            currentPlayerTurn = Player1;
        }
    }

    void ResetGame()
    {
        turnCount = 0;
        currentPlayerTurn = Player1;
        isGameWon = false;
        isGameStarted = false;
        m_GameBoard.ResetBoard();
    }

    IEnumerator ResetTheGame()
    {
        Debug.Log("Game over, starting reset process");

        yield return new WaitForSeconds(5);

        ResetGame();

        Debug.Log("Game has been reset");
    }
}
