using UnityEngine;
using UnityEngine.EventSystems;

public class AccessorySlot : MonoBehaviour, IDropHandler
{
    public AccessoryTypes targetType;
    public AccessoryTags targetTag;

    public DragItem currentlyDraggedItem;

    public GameObject currentGameobject;

    public bool slotIsFull;
    public GameObject slotGameObject;

    public void OnDrop(PointerEventData data)
    {
        currentGameobject = data.pointerDrag;
        currentlyDraggedItem = currentGameobject.GetComponent<DragItem>();

        if (currentGameobject.GetComponent<DragItem>().accessory.accessoryType == targetType)
        {
            Debug.Log("Good");
            currentGameobject.SetActive(false);
            slotIsFull = true;
            slotGameObject = currentGameobject;
        }
        else
        {
            Debug.Log("Not Good");
        }
    }
}
