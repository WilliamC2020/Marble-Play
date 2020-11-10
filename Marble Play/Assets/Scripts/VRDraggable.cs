using UnityEngine;
using UnityEngine.EventSystems;
using Liminal.SDK.VR.Pointers;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using System.Collections;






public class VRDraggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    
    public bool pointerDown;
    public bool pointerOver;
    public bool disabled;
    public GameObject controller;
    public Vector3 xy;
    public IVRInputDevice inputDevice;
    public float rot;
    public bool flipRotateAxis; 
    public enum rotateType
    {
        Rotate,
        None
    }

    public rotateType currentRotateType;


  

    public void OnPointerDown(PointerEventData eventData)
    {
        if (disabled != true)
        {
            Debug.Log("pointer down");
            pointerDown = true;
            StartCoroutine("Drag");
            Debug.Log("pointer down");
        }
       
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (disabled != true)
        {
            Debug.Log("pointer up");
            pointerDown = false;
        }

    }

    public IEnumerator Drag()
    {
        
        var reticle = controller.GetComponentInChildren<ReticleVisual>();
        Debug.Log("blah");
       while (pointerDown == true)
       {   
            
            xy.x = reticle.gameObject.transform.position.x;
            xy.y = reticle.gameObject.transform.position.y;
            xy.z = 0;


            yield return new WaitForFixedUpdate();
            gameObject.transform.position = xy;

           
            
       }

       if (pointerDown == false)
       {
             var x = gameObject.transform.position.x;
             var y = gameObject.transform.position.y;
             Vector3 posXY;
             posXY.x = Mathf.RoundToInt(x);
             posXY.y = Mathf.RoundToInt(y);
             posXY.z = 0;

             gameObject.transform.position = posXY;
       }

            yield return null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerOver = true;
        StartCoroutine("Rotate");
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        pointerOver = false;
    }

    public IEnumerator Rotate()
    {
        var primaryInput = VRDevice.Device.PrimaryInputDevice;

        while (pointerOver == true)
        {



            if (primaryInput.GetButtonDown(VRButton.Back))
            {

                if ((int)currentRotateType == 0)
                {
                    rot += 90;
                }
                else if ((int)currentRotateType == 1)
                {
                    rot += 0;
                }

                if (flipRotateAxis == true)
                {
                    gameObject.transform.rotation = Quaternion.Euler(rot, 90, transform.rotation.z);
                }
                else if (flipRotateAxis == false)
                {
                    gameObject.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rot);
                }



            }

            if (pointerOver == false)
            {

            }

            yield return new WaitForFixedUpdate();
        }
    }
    
}

