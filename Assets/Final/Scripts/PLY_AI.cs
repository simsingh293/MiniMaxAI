using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_AI : MonoBehaviour
{
    public SYS_GameBoard gameBoard;

    public float bestScore = Mathf.NegativeInfinity;

    public void bestMove()
    {
        SYS_BoardNode nodeToSelect;


        for (int i = 0; i < gameBoard.nodeObjs.Count; i++)
        {
            if(gameBoard.nodeObjs[i].GetNodeState() == SYS_BoardNode.nodeState.none)
            {
                
            }
        }
    }
}
