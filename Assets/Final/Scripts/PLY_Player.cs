using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_Player : MonoBehaviour
{
    public SYS_GameBoard gameBoard;

    public void PlayerMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckForInteraction();
        }
    }



    void CheckForInteraction()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 15))
        {
            for (int i = 0; i < gameBoard.gridNodes.Count; i++)
            {
                if (hit.collider.gameObject == gameBoard.gridNodes[i])
                {
                    Debug.Log("Hit a node");
                }
            }

        }
        Debug.DrawLine(ray.origin, ray.origin + (ray.direction * 10));
    }
}
