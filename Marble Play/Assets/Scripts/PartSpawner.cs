using UnityEngine;
using UnityEngine.EventSystems;
using Liminal.SDK.VR.Pointers;
using System.Collections;
using TMPro;

public class PartSpawner : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool pointerDown; // Bool variable that checks if the pointer is being held down.
    public bool newPartSpawned; // Bool variable that checks if a new part has been spawned.
    public GameObject controller; // The controller object on the VRAvatar - the pointer position is extracted from this.
    public VRDraggable objectToSpawn; // The part that this spawner creates.
    public TextMeshProUGUI toolTipDisplayText; // The tooltip to display when hovering over the part to be spawned.
    public Vector3 spawnPosition; // The location to spawn the new part.
    public AudioClip createPart; // Audio clip to play when a part is spawned.
    public AudioSource audioOutput; // The audio source to play the createPart clip through.
    public int[] xRange = new int[2]; // Two-item array of int variables specifying the possible range of x positions for a new part.
    public int[] yRange = new int[2]; // Two-item array of int variables specifying the possible range of y positions for a new part .
    public float partSize; //The size of the spherecast used to detect if the location for the part is already occupied

    void Start()
    {
         
        audioOutput = FindObjectOfType<AudioSource>(); // find the AudioSource to use
        audioOutput.clip = createPart; // set the AudioSource's clip to the clip in createPart
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("pointer down");
        pointerDown = true;
        StartCoroutine("SpawnCheck");
        audioOutput.Stop();
        audioOutput.Play();

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("pointer up");
        pointerDown = false;
        
    }
    public IEnumerator SpawnCheck()
    {
        Debug.Log("check spawn");
        Debug.ClearDeveloperConsole(); //this clears debug messages so that when it crashes the last message seen 
        spawnPosition.x = Random.Range(xRange[0] - 1, xRange[1] + 1);
        Debug.Log("check spawn1");
        Debug.ClearDeveloperConsole();
        spawnPosition.y = Random.Range(yRange[0] - 1, yRange[1] + 1);
        Debug.Log("check spawn2");
        Debug.ClearDeveloperConsole();
        Debug.Log(spawnPosition.x);
        Debug.Log("check spawn3");
        Debug.ClearDeveloperConsole();
        Debug.Log(spawnPosition.y);
        Debug.Log("check spawn4");
        Debug.ClearDeveloperConsole();
        Collider[] hitColliders = Physics.OverlapSphere(spawnPosition, partSize);
        Debug.Log("check spawn5");
        Debug.ClearDeveloperConsole();
        
        if (hitColliders.Length == 0)
        {

            StartCoroutine("Spawn");
        }

        foreach (var item in hitColliders)
        {
            Debug.Log("check spawn6");
            if (item.tag == "Lock@SimStart")
            {

                StartCoroutine("SpawnCheck");
                Debug.Log("fail to place");
            }
            else
            {
               
                StartCoroutine("Spawn");

            }
            
        }
        
        
        yield return null;
        
    }
    
   
    public IEnumerator Spawn()
    {
        

        if (pointerDown == true)
        {
            Debug.Log("start spawn");
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


    