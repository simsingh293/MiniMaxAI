using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boardNode : MonoBehaviour
{
    public (int,int) b_pos;
    [SerializeField]
    public enum b_DesignationType
    {
        X,
        O,
        none
    }

    public b_DesignationType b_CurrentDesignation = b_DesignationType.none;

    public Material b_CurrentMaterial;

    private void Start()
    {
        b_pos.Item1 = (int)(1 + (transform.position.x / 3));
        b_pos.Item2 = (int)(1 + (transform.position.z / 3));

        b_CurrentMaterial = gameObject.GetComponent<Renderer>().material;
    }

    public void ApplyMaterial()
    {
        gameObject.GetComponent<Renderer>().material = b_CurrentMaterial;
    }

}
