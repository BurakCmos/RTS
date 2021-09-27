using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickOn : MonoBehaviour
{
    [SerializeField]
    private Material red;
    [SerializeField]
    private Material green;

    private MeshRenderer mesh;

    public bool alreadySelected = false;
    public NavMeshAgent agent;
    private List<GameObject> selectedList;
    void Start()
    {
        selectedList = new List<GameObject>();
        agent = GetComponent<NavMeshAgent>();
        mesh = GetComponent<MeshRenderer>();
        Camera.main.gameObject.GetComponent<Click>().selectableObjects.Add(this.gameObject);
        OnClick();
    }
    public void OnClick()
    {
        if(!alreadySelected)
            mesh.material = red;
        else
            mesh.material = green;
    }
    private void Update()
    {
        MoveSelectedAgents();
        Formation();
    }
    private void MoveSelectedAgents()
    {
        if (alreadySelected)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    agent.SetDestination(hit.point);
                }
            }
        }
    }
    private void Formation()
    {
        if(alreadySelected)
        {
            selectedList.Add(gameObject);
            foreach(var i in selectedList)
            {
                
            }
        }
    }
}