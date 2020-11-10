using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBridge : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPlay()
    {
        
        var play = GameObject.FindObjectsOfType<TransformReset>();
        foreach (var item in play)
        {
            item.gameObject.SendMessage("OnPlay");
        }
    }
    
    public void OnReset()
    {
        
        var reset = GameObject.FindObjectsOfType<TransformReset>();
        foreach (var item in reset)
        {
            item.gameObject.SendMessage("OnReset");
        }
    }
}
