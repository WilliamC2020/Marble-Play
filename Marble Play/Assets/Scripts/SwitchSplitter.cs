using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSplitter : MonoBehaviour
{
    public BoxCollider left;
    public BoxCollider right;
    public bool leftRight;
    // Start is called before the first frame update
    void Start()
    {
        leftRight = true;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("blah");
        leftRight = !leftRight;
        if (leftRight == true)
        {
            left.enabled = false;
            right.enabled = true;
        }
        
        if (leftRight == false)
        {
            left.enabled = true;
            right.enabled = false;
        }
          
    }       
}