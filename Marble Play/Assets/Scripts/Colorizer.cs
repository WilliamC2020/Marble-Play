using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorizer : MonoBehaviour
{
    public Material recolorMaterial;
  
    void OnTriggerEnter(Collider other)
    {
        var rend = other.gameObject.GetComponent<MeshRenderer>();
        if (rend.gameObject.CompareTag("Marble") == true)
        {
            rend.material = recolorMaterial;
        }
        else
        {
            //space for rent
        }
    }
}
