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


    public void SetNode(boardNode node, GameBoard.Player player, GameBoard board)
    {
        

        node.b_CurrentDesignation = player.designationType;
        node.b_CurrentMaterial = player.material;
        node.ApplyMaterial();

        for (int i = 0; i < board.boardNodes.Count; i++)
        {
            if(board.boardNodes[i] == node)
            {
                board.boardNodes[i] = node;
                break;
            }
        }

        board.currentPlayer = board.players[player.next];
        board.debugger.lastSelectedDesignation = node.b_CurrentDesignation.ToString();

    }
}
