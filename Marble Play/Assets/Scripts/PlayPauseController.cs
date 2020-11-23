
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPauseController : MonoBehaviour
{
    public List<SelfDestruct> doomedGameObjects;
    
    public List<GameObject> moveableGameObjects;
    public List<GameObject> partSpawners;
    public bool playing; 
    public bool reloadLists = false;
    public GameObject PlayButton;
    public GameObject ResetButton;
    public GameObject ClearButton;
    


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("FindSpawners");
        StartCoroutine("ResetState");
        
        PlayButton.GetComponent<ClickableButton>().enabled = true;
        ResetButton.GetComponent<ClickableButton>().enabled = false;
        ClearButton.GetComponent<ClickableButton>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (reloadLists == true)
        {
            
            moveableGameObjects.Clear();
            partSpawners.Clear();
            doomedGameObjects.Clear();
            StartCoroutine("FindSpawners");
            reloadLists = false;
        }

        foreach (var item in partSpawners)
        {
            if (item.GetComponent<PartSpawner>().newPartSpawned == true)
            {  
                moveableGameObjects.Clear();
             
                var play = GameObject.FindGameObjectsWithTag("Lock@SimStart");

                for (int i = 0; i < play.Length; i++)
                {
                    moveableGameObjects.Add(play[i]);
                }

                item.GetComponent<PartSpawner>().newPartSpawned = false;
            }    
        }
    }

    public void EnterPlayState()
    {
        playing = true;
        StartCoroutine("PlayState");
    }

    public void EnterResetState()
    {
        playing = false;
        StartCoroutine("ResetState");
    }
    
    public void EnterClearState()
    {
        playing = false;
        StartCoroutine("ClearState");
    }
    
    public void EnterInitialState()
    {
        playing = false;
        StartCoroutine("ResetState");
    }

    public void NextLevel()
    {
        StartCoroutine("ResetState");
    }
    
    public IEnumerator FindSpawners()
    {
       
        
        var spawn = GameObject.FindGameObjectsWithTag("PartSpawner");
        for (int i = 0; i < spawn.Length; i++)
        {
            partSpawners.Add(spawn[i]);
        }

        yield return null;
    }

    public IEnumerator PlayState()
    {
        PlayButton.GetComponent<ClickableButton>().enabled = false; 
        ResetButton.GetComponent<ClickableButton>().enabled = true;
        ClearButton.GetComponent<ClickableButton>().enabled = false;
        //unlock any gameObjects with rigidbodies
        //lock VRDraggable components
        foreach (var item in moveableGameObjects)
        {
            item.GetComponent<VRDraggable>().disabled = true;
            if (item.GetComponent<Rigidbody>() == true)
            {
                item.GetComponent<Rigidbody>().isKinematic = false;
            }
        }

        yield return null;
    }

    public IEnumerator ResetState()
    {
        PlayButton.GetComponent<ClickableButton>().enabled = true;
        ResetButton.GetComponent<ClickableButton>().enabled = false;
        ClearButton.GetComponent<ClickableButton>().enabled = true;

        //lock rigidbodies
        //unlock VRDraggable components
        foreach (var item in moveableGameObjects)
        {
            item.GetComponent<VRDraggable>().disabled = false;
            if (item.GetComponent<Rigidbody>() == true)
            {
                item.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        yield return null;
    }
    
    public IEnumerator ClearState()
    {
      
        foreach (var item in moveableGameObjects)
        {
            Destroy(item);
            
        }
        moveableGameObjects.Clear();
        yield return null;
    }
}
