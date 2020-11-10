using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


public class SpawnerTooltipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string toolTipText;
    public TextMeshProUGUI toolTipDisplayText;
    LimitedPartSpawner spawn;
    int oldInvNum;
    int newInvNum;
    // Start is called before the first frame update
    void Start()
    {
         spawn = GetComponent<LimitedPartSpawner>();
         oldInvNum = spawn.partsInInventory;
    }
    

    // Update is called once per frame
    void Update()
    {
        newInvNum = spawn.partsInInventory;
        if (newInvNum != oldInvNum)
        {
            toolTipDisplayText.text = toolTipText + " Parts in inventory: " + spawn.partsInInventory;
            oldInvNum = newInvNum;
        }

    }

    

    public void OnPointerEnter(PointerEventData eventData)
    {
        toolTipDisplayText.text = toolTipText + " Parts in inventory: " + spawn.partsInInventory;
        Debug.Log("display tip");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTipDisplayText.text = null;
        Debug.Log("hide tip");
    }
}
