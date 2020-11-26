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
    public float partSize; //The size of the spherecast used to detect if the location for the part is already occupied.

    void Start()
    {
        
        audioOutput = FindObjectOfType<AudioSource>(); // Find the AudioSource to play sound through.
        audioOutput.clip = createPart; // Set the AudioSource's clip to the clip in createPart.
    }

    public void OnPointerDown(PointerEventData eventData) //Start the spawn location checking process, set the pointer down & play the sound.
    {
        Debug.Log("pointer down");
        pointerDown = true; // Set the pointer down.
        StartCoroutine("SpawnCheck"); //Start the spawn location checking process.
        audioOutput.Stop(); //Silence any other sounds playing on the audio source.
        audioOutput.Play(); //Play the sound through the audio source.

    }

    public void OnPointerUp(PointerEventData eventData) 
    {
        Debug.Log("pointer up");
        pointerDown = false; // Set the pointer up.
        
    }

    public IEnumerator SpawnCheck()
    {
        Debug.Log("check spawn");
        Debug.ClearDeveloperConsole(); // Debug.ClearDeveloperConsole() clears debug messages so that when it crashes the last message seen will be the one it crashed on.
        spawnPosition.x = Random.Range(xRange[0] - 1, xRange[1] + 1); // Picks a random x position within the specified range.
        Debug.Log("check spawn1");
        Debug.ClearDeveloperConsole();
        spawnPosition.y = Random.Range(yRange[0] - 1, yRange[1] + 1); // Picks a random y position within the specified range.
        Debug.Log("check spawn2");
        Debug.ClearDeveloperConsole();
        Debug.Log(spawnPosition.x);
        Debug.Log("check spawn3");
        Debug.ClearDeveloperConsole();
        Debug.Log(spawnPosition.y);
        Debug.Log("check spawn4");
        Debug.ClearDeveloperConsole();
        Collider[] hitColliders = Physics.OverlapSphere(spawnPosition, partSize); //do a spherecast of radius partSize at the tentative location spawnPosition and return an array of everything touching it.
        Debug.Log("check spawn5");
        Debug.ClearDeveloperConsole();
        
        if (hitColliders.Length == 0) // The spawning routine fails if there are no parts already in the scene and no colliders are picked up - if this is so, spawn a part to rectif this
        {

            StartCoroutine("Spawn");
        }

        foreach (var item in hitColliders)
        {
            Debug.Log("check spawn6");
            if (item.tag != "Lock@SimStart")
            {
                StartCoroutine("Spawn");
                 yield break;  
            }

            else if (item.tag == "Lock@SimStart")
            {
                StartCoroutine("SpawnCheck");
                Debug.Log("fail to place");
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


    