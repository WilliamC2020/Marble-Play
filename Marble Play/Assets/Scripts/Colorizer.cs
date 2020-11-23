using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colorizer : MonoBehaviour
{
    public Material recolorMaterial;
    public AudioClip note;
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
        other.gameObject.GetComponent<MarbleAudio>().impact = note;
    }
}
