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
    private Vector3 total;
    private Vector3 center;
    private Vector3 startVector;

    void Start()
    {
        selectedList = new List<GameObject>();
        agent = GetComponent<NavMeshAgent>();
        mesh = GetComponent<MeshRenderer>();
        Camera.main.gameObject.GetComponent<Click>().selectableObjects.Add(gameObject);
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
        //MoveSelectedAgents();
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
        //if (agent.stoppingDistance < 3 )
        //{
        //    Debug.Log("hareket ettigi durum");
        //}
        //else
        //{
        //    Debug.Log("durdugu durum");
        //}
    }
    private void Formation()
    {
        if(alreadySelected)
        {
            if (Input.GetMouseButtonDown(1))
            {
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //RaycastHit hit;
                selectedList.Add(gameObject);
                foreach (var i in selectedList)
                {
                    total += i.transform.position;
                    center = total / selectedList.Capacity;
                    startVector = i.transform.position - center;
                }
                MoveAgent(startVector);
                //for (int i = 0; i < selectedList.Count; i++)
                //{
                //    total += selectedList[i].transform.position;
                //    center = total / selectedList.Capacity;
                //    startVector = selectedList[i].transform.position - center;
                //    if (Physics.Raycast(ray, out hit))
                //    {
                //        selectedList[i].GetComponent<NavMeshAgent>().destination = hit.point;
                //    }
                //}
            }
        }
    }
    private void MoveAgent(Vector3 newEndPoint)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            agent.SetDestination(hit.point + newEndPoint);
        }
    }
}