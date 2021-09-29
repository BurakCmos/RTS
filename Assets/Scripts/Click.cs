using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    [SerializeField]
    private LayerMask clickableLayer;
    
    public List<ClickOn> selectedObjects;

    [HideInInspector]
    public List<ClickOn> selectableObjects;

    private Vector3 mousePos1, mousePos2;
    private void Awake()
    {
        selectedObjects = new List<ClickOn>();
        selectableObjects = new List<ClickOn>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
            ClearSelected();

        if (Input.GetMouseButtonDown(0))
        {
            mousePos1 = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            RaycastHit rayHit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, Mathf.Infinity, clickableLayer))
            {
                ClickOn clickOnScript = rayHit.collider.GetComponent<ClickOn>();

                if(Input.GetKey("left ctrl"))
                {
                    if(!clickOnScript.alreadySelected)
                        AddSelected(clickOnScript, rayHit,true);
                    else
                        RemoveSelected(clickOnScript, rayHit, false);
                }
                else
                {
                    ClearSelected();
                    AddSelected(clickOnScript, rayHit,true);
                }
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            mousePos2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            if (mousePos1 != mousePos2)
                SelectObjects();
        }

    }
    private void SelectObjects()
    {
        List<ClickOn> remObjects = new List<ClickOn>();
        if (Input.GetKey("left ctrl") == false)
            ClearSelected();

        Rect selectedRect = new Rect(mousePos1.x, mousePos1.y, mousePos2.x - mousePos1.x, mousePos2.y - mousePos1.y);

        foreach(ClickOn selectObject in selectableObjects)
        {
            if(selectObject != null)
            {
                if (selectedRect.Contains(Camera.main.WorldToViewportPoint(selectObject.transform.position), true))
                {
                    selectedObjects.Add(selectObject);
                    selectObject.GetComponent<ClickOn>().alreadySelected = true;
                    selectObject.GetComponent<ClickOn>().OnClick();
                }
            }
            else
                remObjects.Add(selectObject);
        }
        if(remObjects.Count > 0 )
        {
            foreach (ClickOn rem in remObjects)
            {
                selectableObjects.Remove(rem);
            }
            remObjects.Clear();
        }
    }

    public Vector3 SelectedUnitsCenter()
    {
        Vector3 center = Vector3.zero;
        foreach(var i in selectedObjects)
        {
            center += i.transform.position;
        }
        center /= selectedObjects.Count;
        return center;
    }
    private void ClearSelected() 
    {
        if (selectedObjects.Count > 0)
        {
            foreach (ClickOn obj in selectedObjects)
            {
                obj.GetComponent<ClickOn>().alreadySelected = false;
                obj.GetComponent<ClickOn>().OnClick();
            }
            selectedObjects.Clear();
        }
    }
    private void AddSelected(ClickOn clickOnScript, RaycastHit rayHit,bool alreadySelected)
    {
        selectedObjects.Add(rayHit.collider.gameObject.GetComponent<ClickOn>());
        clickOnScript.alreadySelected = alreadySelected;
        clickOnScript.OnClick();
    }
    private void RemoveSelected(ClickOn clickOnScript, RaycastHit rayHit, bool alreadySelected)
    {
        selectedObjects.Add(rayHit.collider.gameObject.GetComponent<ClickOn>());
        clickOnScript.alreadySelected = alreadySelected;
        clickOnScript.OnClick();
    }
}
