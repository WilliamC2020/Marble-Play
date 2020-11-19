using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleAudio : MonoBehaviour
{
    public AudioClip rolling;
    public AudioClip impact;
    public AudioSource audioOutput;
    public bool isRolling;
    
    // Start is called before the first frame update
    void Start()
    {
        audioOutput = FindObjectOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameObject.GetComponent<Rigidbody>().velocity);
        if (isRolling == true && audioOutput.isPlaying == false)
        {
            
            


                audioOutput.loop = true;
                Debug.Log("bam");
                audioOutput.clip = rolling;
                audioOutput.Play();
            
        
        } 
    }

    public void OnCollisionEnter(Collision collision)
    {
        isRolling = false;
       
        audioOutput.loop = false;
        Debug.Log("bam");
        audioOutput.clip = impact;
        audioOutput.Play();
        
    }

    public void OnCollisionStay(Collision collision)
    {
        isRolling = true;
       
       
    }

    public void OnCollisionExit(Collision collision)
    {
        isRolling = false; 
        audioOutput.Stop();
        Debug.Log("silent");
    }


}

