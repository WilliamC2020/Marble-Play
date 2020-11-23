using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


public class SpawnerTooltipController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string toolTipText;
    public TextMeshProUGUI toolTipDisplayText;
    
    PartSpawner spawn;
    int oldInvNum;
    int newInvNum;

    // Start is called before the first frame update
    void Start()
    {
         spawn = GetComponent<PartSpawner>();
        
    }
    

    // Update is called once per frame
    void Update()
    {
       

    }

    

    public void OnPointerEnter(PointerEventData eventData)
    {
        toolTipDisplayText.text = toolTipText;
        Debug.Log("display tip");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTipDisplayText.text = null;
        Debug.Log("hide tip");
    }
}
