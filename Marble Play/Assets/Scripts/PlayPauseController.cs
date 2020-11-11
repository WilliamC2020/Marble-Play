
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using System.Linq;
public class PlayPauseController : MonoBehaviour
{
    public List<SelfDestruct> doomedGameObjects;
    public List<GameObject> staticGameObjects;
    public List<GameObject> moveableGameObjects;
    public List<GameObject> partSpawners;
    public List<GameObject> puzzles;
    public bool playing; 
    public bool reloadLists = false;
    public GameObject congratsBox;
    public GameObject PlayButton;
    
    

   
    public int levelCount;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("FillLists");
        StartCoroutine("ResetState");
        int levelCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        

        if (reloadLists == true)
        {
            staticGameObjects.Clear();
            moveableGameObjects.Clear();
            partSpawners.Clear();
            doomedGameObjects.Clear();
            StartCoroutine("FillLists");
            reloadLists = false;
        }

        foreach (var item in partSpawners)
        {
                if (item.GetComponent<LimitedPartSpawner>().newPartSpawned == true)
                {  
                moveableGameObjects.Clear();
             
                var play = GameObject.FindGameObjectsWithTag("Lock@SimStart");

                for (int i = 0; i < play.Length; i++)
                {
                    moveableGameObjects.Add(play[i]);
                }

                item.GetComponent<LimitedPartSpawner>().newPartSpawned = false;
                }    
        }
    }

    public void EnterPlayState()
    {
        playing = true;
        StartCoroutine("PlayState");
    }

  //  public void EnterPauseState()
  //  {
   //     playing = false;
  //      StartCoroutine("PauseState");
  //  }

    public void EnterResetState()
    {
        playing = false;
        StartCoroutine("ResetState");
    }
    
    public void EnterInitialState()
    {
        playing = false;
        StartCoroutine("ResetState");
    }
    public void EnterWinState()
    {
        StartCoroutine("ShowCongratsBox");
        
    }

    public void NextLevel()
    {
        StartCoroutine("ResetState");
    }

    public IEnumerator ShowCongratsBox()
    {
        congratsBox.SetActive(true);
        yield return new WaitForSeconds(5);
        congratsBox.SetActive(false);
        
       
        var kill = GameObject.FindObjectsOfType<SelfDestruct>();
        for (int i = 0; i < kill.Length; i++)
        {
            doomedGameObjects.Add(kill[i]);
        }

        foreach (var item in doomedGameObjects)
        {
            item.SendMessage("Terminate");
        }

        reloadLists = true;
        puzzles[levelCount].SetActive(false);
        levelCount += 1;
        SendMessage("EnterResetState");
        puzzles[levelCount].SetActive(true);






        yield return null;
    }
    
    

    

    public IEnumerator FillLists()
    {
        var pause = GameObject.FindGameObjectsWithTag("Lock@SimPause");
        for (int i = 0; i < pause.Length; i++)
        {
            staticGameObjects.Add(pause[i]);
        }
        
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

        foreach (var item in staticGameObjects)
        {
            item.GetComponent<VRDraggable>().disabled = true;
            if (item.GetComponent<Rigidbody>() == true)
            {
                item.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
        yield return null;
    }

   // public IEnumerator PauseState()
    //{
        //lock rigidbodies 
        //do not unlock VRDraggable components
       // foreach (var item in moveableGameObjects)
      //  {
            
           // if (item.GetComponent<Rigidbody>() == true)
       //     {
             //   item.GetComponent<Rigidbody>().isKinematic = true;
       //     }

    //    }

      //  foreach (var item in staticGameObjects)
      //  {
           
      //      if (item.GetComponent<Rigidbody>() == true)
     //       {
         //       item.GetComponent<Rigidbody>().isKinematic = true;
      //      }

      //  }
    //    yield return null;
   // }

    public IEnumerator ResetState()
    {
        PlayButton.GetComponent<ClickableButton>().enabled = true;

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

        foreach (var item in staticGameObjects)
        {
            item.GetComponent<VRDraggable>().disabled = false;
            if (item.GetComponent<Rigidbody>() == true)
            {
                item.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
        yield return null;
    }
}
