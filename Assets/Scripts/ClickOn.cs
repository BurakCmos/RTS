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
    private int numOfSelected;
    public Click click;
    public Vector3 targetPos { get; set; }

    void Start()
    {
        click = Camera.main.gameObject.GetComponent<Click>();
        selectedList = new List<GameObject>();
        agent = GetComponent<NavMeshAgent>();
        mesh = GetComponent<MeshRenderer>();
        Camera.main.gameObject.GetComponent<Click>().selectableObjects.Add(gameObject.GetComponent<ClickOn>());
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
        if (alreadySelected)
        {
            if (Input.GetMouseButtonDown(1))
            {
                MyTestFunction();
                MoveAgent(targetPos);
            }
        }
        //if (alreadySelected)
        //{
        //    if (Input.GetMouseButtonDown(1))
        //    {
        //        ArrangeUnitsInStaggeredFormation();
        //        //ArrangeUnitsInLineFormation();
        //        MoveAgent(targetPos);
        //        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //        //RaycastHit hit;

        //        //if (Physics.Raycast(ray, out hit))
        //        //{
        //        //    targetPos = hit.point;
        //        //    agent.SetDestination(targetPos);
        //        //}
        //    }
        //}
                //MoveSelectedAgents();
                //if (alreadySelected)
                //{
                //    //if (Input.GetMouseButtonDown(1))
                //    //{
                //        numOfSelected = Camera.main.gameObject.GetComponent<Click>().selectedObjects.Count;
                //        List<GameObject> units = Camera.main.gameObject.GetComponent<Click>().selectedObjects;
                //        Vector3 unitCenter = Camera.main.gameObject.GetComponent<Click>().SelectedUnitsCenter();
                //        float leftmostXOffset = -1 * (numOfSelected / 2.0f) * 3.0f;
                //        for(int i = 0; i < numOfSelected; ++i)
                //        {
                //            float XOffset = leftmostXOffset + i * 3.0f;
                //            Vector3 targetPos = unitCenter;
                //            targetPos.x += XOffset;
                //            units[i].GetComponent<NavMeshAgent>().destination = targetPos;
                //        }
                //    //}
                //}
                //Formation();
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
            if (Input.GetMouseButtonDown(1))
            {
                //List<UnitScript> units = selectionMgr.SelectedUnits;
                //Vector3 unitCenter = selectionMgr.SelectedUnitsCenter;
                //int numUnits = units.Count;

                //float leftmostXOffset = -1 * (numUnits / 2.0f) * UnitSpacingInFormation;
                //for (int i = 0; i < numUnits; ++i)
                //{
                //    float xOffset = leftmostXOffset + i * UnitSpacingInFormation;

                //    Vector3 targetPos = unitCenter;
                //    targetPos.x += xOffset;
                //    units[i].TargetPos = targetPos;
                //}
                //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //RaycastHit hit;
                //selectedList.Add(gameObject);
                total = Vector3.zero;
                startVector = Vector3.zero;
                //foreach (var i in selectedList)
                //{
                //    total += i.transform.position;
                //    startVector += i.GetComponent<NavMeshAgent>().destination;
                //}
                //total += gameObject.transform.position;
                //total /= 2;
                //startVector += gameObject.GetComponent<NavMeshAgent>().destination;
                //startVector /= 2;
                //MoveAgent(startVector,total);
                //center = total / selectedList.Count;
                //startVector = i.transform.position - center;
                //Vector3 difference = 
                List<ClickOn> units = click.selectedObjects;
                Vector3 unitCenter = click.SelectedUnitsCenter();
                int numUnits = units.Count;
                for (int i = 0; i < numUnits; i++)
                {
                    //total += gameObject.transform.position;
                    //center = total / selectedList.Capacity;
                    startVector = gameObject.transform.position;/* - unitCenter;*/
                    //if (Physics.Raycast(ray, out hit))
                    //{
                    //    selectedList[i].GetComponent<NavMeshAgent>().destination = hit.point;
                    //}
                    //}
                    //MoveAgent(startVector);
                }
            }
        }
    }
    private void MoveAgent(Vector3 newEndPoint)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            agent.SetDestination((transform.position + hit.point) - newEndPoint);
        }
    }
    private void MyTestFunction()
    {
        List<ClickOn> units = click.selectedObjects;
        Vector3 unitCenter = click.SelectedUnitsCenter();
        int numUnits = units.Count;

        float leftmostXOffset = -1 * (numUnits / 2.0f);
        for (int i = 0; i < numUnits; i++)
        {
            float xOffset = 0;
            float zOffset = 2;
            Vector3 targetPos = unitCenter;
            targetPos.x += xOffset;
            targetPos.z += zOffset;
            units[i].targetPos = targetPos;
        }
    }
    private void ArrangeUnitsInStaggeredFormation()
    {
        List<ClickOn> units = click.selectedObjects;
        Vector3 unitCenter = click.SelectedUnitsCenter();
        int numUnits = units.Count;

        float leftmostXOffset = -1 * (numUnits / 2.0f) * 3f;
        for (int i = 0; i < numUnits; ++i)
        {
            float xOffset = leftmostXOffset + i * 3f;
            float zOffset = 0;
            if (IsOdd(i))
            {
                zOffset = 3f;
            }

            Vector3 targetPos = unitCenter;
            targetPos.x += xOffset;
            targetPos.z += zOffset;
            units[i].targetPos = targetPos;
        }
    }
    private void ArrangeUnitsInLineFormation()
    {
        List<ClickOn> units = click.selectedObjects;
        Vector3 unitCenter = click.SelectedUnitsCenter();
        int numUnits = units.Count;

        float leftmostXOffset = -1 * (numUnits / 2.0f) * 3;
        for (int i = 0; i < numUnits; ++i)
        {
            float xOffset = leftmostXOffset + i * 3;

            Vector3 targetPos = unitCenter;
            targetPos.x += xOffset;
            units[i].targetPos = targetPos;
        }
    }
    private bool IsOdd(int val)
    {
        return (val % 2) == 1;
    }
}