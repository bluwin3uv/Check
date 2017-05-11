using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGL;

public class PicePicker : MonoBehaviour
{
    public float piceHeight = 5f;
    public float rayDist = 1000f;
    public LayerMask selectionIgnoreLayer;
    private Pic selected;
    private CheckerBoard board;

    void Start()
    {
        board = FindObjectOfType<CheckerBoard>();
        if (board == null)
        {
            Debug.LogError("No Board");
        }
    }

    void CheckSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        GizmosGL.color = Color.red;
        GizmosGL.AddLine(ray.origin, ray.origin + ray.direction * rayDist,0.1f,0.1f);
        RaycastHit hit;
        if(Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(ray,out hit, rayDist))
            {
                selected = hit.collider.GetComponent<Pic>();
                if(selected == null)
                {
                    Debug.Log("AAAAAAAHHHHHH NOOOO!!" + hit.collider.name);
                }
            }
        }
    }

    void MoveSelection()
    {
        if(selected != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            GizmosGL.color = Color.yellow;
            GizmosGL.AddLine(ray.origin, ray.origin + ray.direction * rayDist, 0.1f, 0.1f);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit, rayDist, ~selectionIgnoreLayer))
            {
                GizmosGL.color = Color.blue;
                GizmosGL.AddSphere(hit.point, 0.5f);
                Vector3 picePos = hit.point + Vector3.up * piceHeight;
                selected.transform.position = picePos;
            }
        }
    } 

    void Update()
    {
        CheckSelection();
        MoveSelection();
    }
}
