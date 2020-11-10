using UnityEngine;
using UnityEngine.EventSystems;
using Liminal.SDK.VR.Pointers;
using System.Collections;
using TMPro;

public class PartSpawner : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool pointerDown;
    public GameObject controller;
    public VRDraggable objectToSpawn;
    public TextMeshProUGUI toolTipDisplayText;
    public Vector3 spawnPosition;

    public void Start()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("pointer down");
        pointerDown = true;
        StartCoroutine("Spawn");
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("pointer up");
        pointerDown = false;
        
    }

    
   
    public IEnumerator Spawn()
    {
        
        if (pointerDown == true)
        {
            VRDraggable vr = Instantiate(objectToSpawn, spawnPosition, transform.rotation);
            vr.gameObject.GetComponent<TooltipController>().toolTipDisplayText = toolTipDisplayText;
            vr.gameObject.GetComponent<TooltipController>().toolTipText = gameObject.GetComponent<TooltipController>().toolTipText;
            vr.gameObject.tag = "Lock@SimStart";
            vr.controller = controller;
            
        }

        if (pointerDown == false)
        {
           
        }
        
      yield return null;
    }
}


    