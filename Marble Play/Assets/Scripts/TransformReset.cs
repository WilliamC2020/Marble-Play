using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformReset : MonoBehaviour
{
    public Vector3 position;
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
        position = gameObject.transform.position;
        
    }
    
    public void OnReset()
    {
        gameObject.transform.position = position;
        
    }

}
