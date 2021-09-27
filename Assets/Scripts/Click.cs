using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    [SerializeField]
    private LayerMask clickableLayer;
    
    public List<GameObject> selectedObjects;

    [HideInInspector]
    public List<GameObject> selectableObjects;

    private Vector3 mousePos1, mousePos2;
    private void Awake()
    {
        selectedObjects = new List<GameObject>();
        selectableObjects = new List<GameObject>();
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(1))
        //    ClearSelected();
        
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
        List<GameObject> remObjects = new List<GameObject>();
        if (Input.GetKey("left ctrl") == false)
            ClearSelected();

        Rect selectedRect = new Rect(mousePos1.x, mousePos1.y, mousePos2.x - mousePos1.x, mousePos2.y - mousePos1.y);

        foreach(GameObject selectObject in selectableObjects)
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
            foreach (GameObject rem in remObjects)
            {
                selectableObjects.Remove(rem);
            }
            remObjects.Clear();
        }
    }

    private void ClearSelected() 
    {
        if (selectedObjects.Count > 0)
        {
            foreach (GameObject obj in selectedObjects)
            {
                obj.GetComponent<ClickOn>().alreadySelected = false;
                obj.GetComponent<ClickOn>().OnClick();
            }
            selectedObjects.Clear();
        }
    }
    private void AddSelected(ClickOn clickOnScript, RaycastHit rayHit,bool alreadySelected)
    {
        selectedObjects.Add(rayHit.collider.gameObject);
        clickOnScript.alreadySelected = alreadySelected;
        clickOnScript.OnClick();
    }
    private void RemoveSelected(ClickOn clickOnScript, RaycastHit rayHit, bool alreadySelected)
    {
        selectedObjects.Add(rayHit.collider.gameObject);
        clickOnScript.alreadySelected = alreadySelected;
        clickOnScript.OnClick();
    }
}
