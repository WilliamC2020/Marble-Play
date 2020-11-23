using UnityEngine;
using UnityEngine.EventSystems;
using Liminal.SDK.VR.Pointers;
using System.Collections;
using TMPro;

public class LimitedPartSpawner : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool pointerDown; //is the pointer over the object & being held down?
    public bool newPartSpawned; //was a new part spawned?
    public GameObject controller; //the vr controller. the controller variable of the new part is set to this
    public VRDraggable objectToSpawn; //the object to spawn when clicked
    public TextMeshProUGUI toolTipDisplayText; //the text to display when moused over
    public Vector3 spawnPosition; //where to spawn the new part
    public int partsInInventory; //how many parts of this type are available for use
    
    public void Start()
    {
        
        newPartSpawned = false;
    }

    //check if the pointer was held down
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("pointer down");
        pointerDown = true;

        //spawn the new part
        StartCoroutine("Spawn");
        newPartSpawned = true;
    }

    //check if the pointer was released
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("pointer up");
        pointerDown = false;
        
    }

    
   
    public IEnumerator Spawn()
    {
        //if pointer held down & number of available parts greater than 0
        if (pointerDown == true && partsInInventory > 0)
        {
            
            VRDraggable vr = Instantiate(objectToSpawn, spawnPosition, transform.localRotation);
            vr.gameObject.GetComponent<TooltipController>().toolTipDisplayText = toolTipDisplayText;
            vr.gameObject.GetComponent<TooltipController>().toolTipText = gameObject.GetComponent<SpawnerTooltipController>().toolTipText;
            vr.gameObject.GetComponent<TooltipController>().parentPartSpawner = gameObject;
            vr.controller = controller;
            vr.rot = gameObject.transform.rotation.eulerAngles.z;
            vr.gameObject.tag = "Lock@SimStart";
            partsInInventory = partsInInventory - 1;
            
        }

        if (pointerDown == false)
        {
           
        }
        
      yield return null;
    }
}


    