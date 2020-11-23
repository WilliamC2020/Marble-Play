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
    public bool newPartSpawned;
    public AudioClip createPart;
    public AudioSource audioOutput;
    void Start()
    {
        audioOutput = FindObjectOfType<AudioSource>();
        audioOutput.clip = createPart;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("pointer down");
        pointerDown = true;
        StartCoroutine("Spawn");
        audioOutput.Stop();
        audioOutput.Play();

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
            VRDraggable vr = Instantiate(objectToSpawn, spawnPosition, transform.localRotation);
            vr.gameObject.GetComponent<TooltipController>().toolTipDisplayText = toolTipDisplayText;
            vr.gameObject.GetComponent<TooltipController>().toolTipText = gameObject.GetComponent<SpawnerTooltipController>().toolTipText;
            vr.gameObject.GetComponent<TooltipController>().parentPartSpawner = gameObject;
            vr.controller = controller;
            vr.rot = gameObject.transform.rotation.eulerAngles.z;
            vr.gameObject.tag = "Lock@SimStart";
            newPartSpawned = true;
        }

        if (pointerDown == false)
        {
           
        }
        
      yield return null;
    }
}


    