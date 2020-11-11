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
            //instantiate new part at specified location & parent part spawner's rotation
            VRDraggable vr = Instantiate(objectToSpawn, spawnPosition, transform.rotation);

            //set the text object the child uses to display tooltips
            vr.gameObject.GetComponent<TooltipController>().toolTipDisplayText = toolTipDisplayText;

            //set the tootip text of the child to match its parent
            vr.gameObject.GetComponent<TooltipController>().toolTipText = gameObject.GetComponent<SpawnerTooltipController>().toolTipText;

            //tell the child that this is its parent
            vr.gameObject.GetComponent<TooltipController>().parentPartSpawner = gameObject;

            // set the child's controller to match its parent
            vr.controller = controller;
            vr.rot = gameObject.transform.rotation.eulerAngles.z;
            Debug.Log(gameObject.transform.rotation.z);
            //set its tag so it is rendered immovable
            vr.gameObject.tag = "Lock@SimStart";
            partsInInventory = partsInInventory - 1;
            
        }

        if (pointerDown == false)
        {
           
        }
        
      yield return null;
    }
}


    