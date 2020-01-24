using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    public GameObject nodePrefab;
    public Material X_Material;
    public Material O_Material;
    public Material None_Material;
    public List<GameObject> nodes = new List<GameObject>();

    Gameplay gameplay;

    public bool gameStarted = false;

    public enum PlayerType
    {
        PlayerOne,
        PlayerTwo,
        AI
    }

    public PlayerType currentPlayer = PlayerType.PlayerOne;


    // Start is called before the first frame update
    void Start()
    {
        GenerateBoard();

        gameplay = Gameplay.singleton;

        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckForInteraction();
        }

        if (CheckForFullBoard() && gameStarted)
        {
            ResetBoard();
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

                GameObject temp = Instantiate(nodePrefab, this.transform);

                Vector3 position = Vector3.zero;

                position.x = i * 3;
                position.z = j * 3;

                temp.transform.position = position;

                temp.name = "Node " + count;

                nodes.Add(temp);
            }
        }
    }

    void ResetBoard()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            nodes[i].GetComponent<boardNode>().b_CurrentDesignation = boardNode.b_DesignationType.none;
            nodes[i].GetComponent<boardNode>().b_CurrentMaterial = None_Material;
            nodes[i].GetComponent<boardNode>().ApplyMaterial();
        }

        gameStarted = false;

        currentPlayer = PlayerType.PlayerOne;
    }


    void CheckForInteraction()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 10))
        {
            if (hit.collider.gameObject.GetComponent<boardNode>())
            {
                if (!gameStarted)
                {
                    gameStarted = true;
                }


                boardNode node = hit.collider.gameObject.GetComponent<boardNode>();

                //Debug.Log(node.b_pos + " : " + node.b_CurrentDesignation);
                Debug.Log(gameplay.CheckNodeAvailability(node));

                if (gameplay.CheckNodeAvailability(node))
                {
                    gameplay.SetNode(node, this);
                }

            }


        }

        Debug.DrawLine(ray.origin, ray.origin + (ray.direction * 10));
    }


    bool CheckForFullBoard()
    {
        bool result = true;


        for (int i = 0; i < nodes.Count; i++)
        {
            if(nodes[i].GetComponent<boardNode>().b_CurrentDesignation == boardNode.b_DesignationType.none)
            {
                result = false;
                break;
            }
        }


        return result;
    }
}
