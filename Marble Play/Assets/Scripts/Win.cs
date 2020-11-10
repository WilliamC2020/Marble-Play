using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{

    public PlayPauseController playPause;
    public GameObject nextPuzzle;
    public GameObject thisPuzzle;
    public List<SelfDestruct> doomedGameObjects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

   
    
        
       
    

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine("NextPuzzle", other);
    }          

    public IEnumerator NextPuzzle(Collider other)
    {    
        if (playPause.playing == true && other.gameObject.layer == 8)
        {
           
            
       
            Debug.LogWarning("Win");
           
            
            
           
                
             
            
             playPause.SendMessage("EnterWinState");
                 
           
            
            
            
           /* Debug.Log("You Win!");
            


            foreach (var item in doomedGameObjects)
            {
                item.SendMessage("Terminate");
            }*/
        }

        //nextPuzzle.SetActive(true);
        //playPause.SendMessage("EnterResetState");
        //thisPuzzle.SetActive(false);
        yield return null;
    }
}