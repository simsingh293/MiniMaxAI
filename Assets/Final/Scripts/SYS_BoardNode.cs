using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYS_BoardNode : MonoBehaviour
{
    public enum nodeState 
    { 
        none,
        X,
        O
    }

    public Vector2 nodePosition = Vector2.zero;

    [SerializeField]
    private nodeState currentState = nodeState.none;

    [SerializeField] private GameObject Cross;
    [SerializeField] private GameObject Naught;

    public nodeState GetNodeState()
    {
        return currentState;
    }

    public void SetNodeState(nodeState state)
    {
        currentState = state;
    }

    public Vector2 GetNodePosition()
    {
        return nodePosition;
    }

    public void SetNodePosition(int row, int col)
    {
        nodePosition.x = row;
        nodePosition.y = col;
    }

    public void ActivateSymbol()
    {
        if(currentState == nodeState.X)
        {
            Cross?.SetActive(true);
            Naught?.SetActive(false);
        }
        else if(currentState == nodeState.O)
        {
            Naught?.SetActive(true);
            Cross?.SetActive(false);
        }
        else
        {
            Naught?.SetActive(false);
            Cross?.SetActive(false);
        }
    }
}
