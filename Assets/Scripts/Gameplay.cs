using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay
{
    private static Gameplay _instance;
    public static Gameplay singleton 
    {
        get
        {
            if(_instance == null)
            {
                _instance = new Gameplay();
            }
            return _instance;
        } 
        
    }


    void SetPlayers()
    {

    }


    public bool CheckNodeAvailability(boardNode node)
    {
        bool result = false;

        if(node.b_CurrentDesignation == boardNode.b_DesignationType.none)
        {
            result = true;
        }
        else
        {
            result = false;
        }


        return result;
    }


    public void SetNode(boardNode node, GameBoard board)
    {
        if(board.currentPlayer == GameBoard.PlayerType.PlayerOne)
        {
            // Player 1 code
            node.b_CurrentDesignation = boardNode.b_DesignationType.X;
            node.b_CurrentMaterial = board.X_Material;
            node.ApplyMaterial();

            board.currentPlayer = GameBoard.PlayerType.PlayerTwo;
        }
        else if(board.currentPlayer == GameBoard.PlayerType.PlayerTwo)
        {
            // player 2 code
            node.b_CurrentDesignation = boardNode.b_DesignationType.O;
            node.b_CurrentMaterial = board.O_Material;
            node.ApplyMaterial();

            board.currentPlayer = GameBoard.PlayerType.PlayerOne;
        }
        else
        {
            // AI code
        }
    }
}
