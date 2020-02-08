using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MNode
{
    public int x;
    public int y;
    public int player;
}

public class MovesAndScores
{
    public int score;
    public MNode point;

    public MovesAndScores(int _score, MNode _point)
    {
        this.score = _score;
        this.point = _point;
    }
}


public class minimax : MonoBehaviour
{
    public GameObject xPrefab;
    public GameObject oPrefab;
    MNode[,] grid = new MNode[3, 3];
    public List<MovesAndScores> rootChildrenScores;
    public GameObject ui;


    void Update()
    {






    }

    public bool hasOWon()
    {
        if (grid[0, 0] != null && grid[1, 1] != null && grid[2, 2] != null)
        {
            if (grid[0, 0].player == grid[1, 1].player && grid[0, 0].player == grid[2, 2].player && grid[0, 0].player == 1)
            {
                return true;
            }
        }

        if (grid[0, 2] != null && grid[1, 1] != null && grid[2, 0] != null)
        {
            //diagonal win
            if (grid[0, 2].player == grid[1, 1].player && grid[0, 2].player == grid[2, 0].player && grid[0, 2].player == 1)
                return true;
        }




        return false;
    }
    public bool hasXWon()
    {


        return false;

    }





    

    List<MNode> getMoves()
    {
        List<MNode> result = new List<MNode>();

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                if(grid[row,col] == null)
                {
                    MNode newNode = new MNode();
                    newNode.x = row;
                    newNode.y = col;

                    result.Add(newNode);
                }
            }
        }

        return result;
    }
}
