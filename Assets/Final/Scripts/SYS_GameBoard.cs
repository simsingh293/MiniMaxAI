using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SYS_GameBoard : MonoBehaviour
{
    public GameObject gridHolder;
    public GameObject nodePrefab;

    public List<GameObject> gridNodes;
    public List<SYS_BoardNode> nodeObjs;

    public float resetTime = 3;

    public bool isBoardFull = false;

    Vector2 start = new Vector2(-3, -3);

    List<List<SYS_BoardNode>> linesToCheck = new List<List<SYS_BoardNode>>();


    public List<SYS_BoardNode> row_1 = new List<SYS_BoardNode>();
    public List<SYS_BoardNode> row_2 = new List<SYS_BoardNode>();
    public List<SYS_BoardNode> row_3 = new List<SYS_BoardNode>();
    
    public List<SYS_BoardNode> col_1 = new List<SYS_BoardNode>();
    public List<SYS_BoardNode> col_2 = new List<SYS_BoardNode>();
    public List<SYS_BoardNode> col_3 = new List<SYS_BoardNode>();

    public List<SYS_BoardNode> diag_right = new List<SYS_BoardNode>();
    public List<SYS_BoardNode> diag_left = new List<SYS_BoardNode>();


    public List<SYS_BoardNode> winLine = new List<SYS_BoardNode>();

    private void Awake()
    {
        GenerateBoard();
        SetupWinChecks();
    }


    private void Update()
    {
        isBoardFull = CheckForFullBoard();

    }

    void GenerateBoard()
    {
        Vector2 pos = start;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                GameObject temp = Instantiate(nodePrefab, new Vector3(pos.x, 0, pos.y), Quaternion.identity, gridHolder.transform);
                SYS_BoardNode temp2 = temp.GetComponent<SYS_BoardNode>();

                temp.name = "Row: " + (i + 1) + " - " + "Col: " + (j+ 1);

                gridNodes.Add(temp);


                temp2.SetNodePosition((i + 1), (j + 1));

                nodeObjs.Add(temp2);

                pos.y += 3;
            }
            pos.y = start.y;
            pos.x += 3;


        }
    }


    void SetupWinChecks()
    {
        GetRow(row_1, 1);
        GetRow(row_2, 2);
        GetRow(row_3, 3);

        GetCol(col_1, 1);
        GetCol(col_2, 2);
        GetCol(col_3, 3);

        GetRightDiag(diag_right);
        GetLeftDiag(diag_left);

        linesToCheck.Add(row_1);
        linesToCheck.Add(row_2);
        linesToCheck.Add(row_3);

        linesToCheck.Add(col_1);
        linesToCheck.Add(col_2);
        linesToCheck.Add(col_3);

        linesToCheck.Add(diag_left);
        linesToCheck.Add(diag_right);
    }

    bool CheckForFullBoard()
    {
        for (int i = 0; i < nodeObjs.Count; i++)
        {
            if(nodeObjs[i].GetNodeState() == SYS_BoardNode.nodeState.none)
            {
                return false;
            }
        }

        return true;
    }

    void GetRow(List<SYS_BoardNode> nodes, int rowNumber)
    {
        for (int i = 0; i < nodeObjs.Count; i++)
        {
            if (nodeObjs[i].GetNodePosition().x == rowNumber && !nodes.Contains(nodeObjs[i]))
            {
                nodes.Add(nodeObjs[i]);
            }
        }
    }

    void GetCol(List<SYS_BoardNode> nodes, int colNumber)
    {
        for (int i = 0; i < nodeObjs.Count; i++)
        {
            if (nodeObjs[i].GetNodePosition().y == colNumber && !nodes.Contains(nodeObjs[i]))
            {
                nodes.Add(nodeObjs[i]);
            }
        }
    }

    void GetRightDiag(List<SYS_BoardNode> nodes)
    {
        for (int i = 0; i < nodeObjs.Count; i++)
        {
            if(nodeObjs[i].GetNodePosition().x == nodeObjs[i].GetNodePosition().y)
            {
                nodes.Add(nodeObjs[i]);
            }
        }
    }

    void GetLeftDiag(List<SYS_BoardNode> nodes)
    {
        for (int i = 0; i < nodeObjs.Count; i++)
        {
            if (nodeObjs[i].GetNodePosition().x == 1 && nodeObjs[i].GetNodePosition().y == 3)
            {
                nodes.Add(nodeObjs[i]);
            }

            if (nodeObjs[i].GetNodePosition().x == 2 && nodeObjs[i].GetNodePosition().y == 2)
            {
                nodes.Add(nodeObjs[i]);
            }

            if (nodeObjs[i].GetNodePosition().x == 3 && nodeObjs[i].GetNodePosition().y == 1)
            {
                nodes.Add(nodeObjs[i]);
            }
        }
    }

    bool CheckList(List<SYS_BoardNode> nodes)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            if(nodes[i].GetNodeState() == SYS_BoardNode.nodeState.none)
            {
                return false;
            }
        }

        if(nodes[0].GetNodeState() == nodes[1].GetNodeState() && nodes[1].GetNodeState() == nodes[2].GetNodeState())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckForWin()
    {
        bool result = false;


        for (int i = 0; i < linesToCheck.Count; i++)
        {
            result = CheckList(linesToCheck[i]);

            if (result)
            {
                winLine = linesToCheck[i];
                break;
            }
        }

        return result;
    }

    public void ResetBoard()
    {
        for (int i = 0; i < nodeObjs.Count; i++)
        {
            nodeObjs[i].SetNodeState(SYS_BoardNode.nodeState.none);
            nodeObjs[i].ActivateSymbol();
        }
    }

}
