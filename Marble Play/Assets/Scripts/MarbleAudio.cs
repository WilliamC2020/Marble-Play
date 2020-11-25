using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleAudio : MonoBehaviour
{
    public AudioClip rolling;
    public AudioClip impact;
    public AudioSource audioOutput;
    public bool isRolling;
    public int playing;

    // Start is called before the first frame update
    void Start()
    {
        audioOutput = FindObjectOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (isRolling == true && audioOutput.isPlaying == false && gameObject.GetComponent<Rigidbody>().velocity.magnitude > 10)
        {
            
            


                audioOutput.loop = true;
                Debug.Log("bam");
                audioOutput.clip = rolling;
                audioOutput.Play();
            
        
        } */
       
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 10)
        {
            isRolling = false;
       
            audioOutput.loop = false;

            StartCoroutine("Play");
        }
        
    }

    public void OnCollisionStay(Collision collision)
    {
        isRolling = true;
       
       
    }

    public void OnCollisionExit(Collision collision)
    {
        isRolling = false; 
        

    }
     public IEnumerator Play()
    {
        playing += 1;
        audioOutput.PlayOneShot(impact);
        yield return new WaitForSeconds(impact.length);
        playing -= 1;
    }

}

