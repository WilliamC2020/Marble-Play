﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceZone : MonoBehaviour
{
    public Vector3 force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void OnTriggerStay(Collider other)
    {
        other.attachedRigidbody.AddForce(force);
    }
}
