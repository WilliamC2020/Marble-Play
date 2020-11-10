using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


public class TooltipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string toolTipText;
    public TextMeshProUGUI toolTipDisplayText;
    public GameObject parentPartSpawner;
    int oldInvNum;
    int newInvNum;
    // Start is called before the first frame update
    void Start()
    {
        oldInvNum = parentPartSpawner.GetComponent<LimitedPartSpawner>().partsInInventory;
    }

    // Update is called once per frame
    void Update()
    {
        newInvNum = parentPartSpawner.GetComponent<LimitedPartSpawner>().partsInInventory;
        if (newInvNum != oldInvNum)
        {
           toolTipDisplayText.text = toolTipText + " Parts in inventory: " + parentPartSpawner.GetComponent<LimitedPartSpawner>().partsInInventory;
           oldInvNum = newInvNum;
        }
         
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        toolTipDisplayText.text = toolTipText + " Parts in inventory: " + parentPartSpawner.GetComponent<LimitedPartSpawner>().partsInInventory;
        Debug.Log("display tip");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTipDisplayText.text = null;
        Debug.Log("hide tip");
    }

    
   
       
   
}
