using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColorizer : MonoBehaviour
{

    public List<Material> recolorMaterials;
  
    void OnTriggerEnter(Collider other)
    {
        var rend = other.gameObject.GetComponent<MeshRenderer>();
        if (rend.gameObject.layer == 8)
        {
            rend.material = recolorMaterials[Random.Range(-1, recolorMaterials.Count)];
        }
        else
        {
            //space for rent
        }
    }
}
