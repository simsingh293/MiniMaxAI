using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boardNode : MonoBehaviour
{
    protected GameObject obj;
    protected Transform objTransform;
    protected TextMesh text;
    protected Renderer objRenderer;


    [SerializeField]
    public (int,int) b_pos;


    public enum b_DesignationType
    {
        X,
        O,
        none
    }

    public b_DesignationType b_CurrentDesignation = b_DesignationType.none;

    public Material b_CurrentMaterial;



    public void Init()
    {
        obj = gameObject;
        objTransform = gameObject.transform;
        objRenderer = GetComponent<MeshRenderer>();
        text = GetComponentInChildren<TextMesh>();

        b_pos.Item1 = (int)(1 + (objTransform.position.x / 3));
        b_pos.Item2 = (int)(1 + (objTransform.position.z / 3));

        b_CurrentMaterial = obj.GetComponent<Renderer>().material;

        text.text = b_pos.ToString();

    }

    public void ApplyMaterial()
    {
        objRenderer.material = b_CurrentMaterial;
    }

}
