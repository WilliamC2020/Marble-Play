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
    public PlayPauseController playPauseController; 

    [Tooltip("The range of x values a new part can spawn at !!WARNING!! Ensure that the range values are large enough to allow parts enough space to spawn - failure to do so will cause a infinite loop on spawning 2 parts!!!")]
    public int[] xRange = new int[2];

    [Tooltip("The range of y values a new part can spawn at !!WARNING!! Ensure that the range values are large enough to allow parts enough space to spawn - failure to do so will cause a infinite loop on spawning 2 parts!!!")]
    public int[] yRange = new int[2];
    
    [Tooltip(" The size of the spherecast check used to detemine where to place parts ")]
    public float partSize;
    void Start()
    {
         
        audioOutput = FindObjectOfType<AudioSource>();
        audioOutput.clip = createPart;
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


    