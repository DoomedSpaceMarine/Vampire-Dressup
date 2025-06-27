using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AccessorySlot : MonoBehaviour, IDropHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public AccessoryTypes targetType;
    public AccessoryTags targetTag;

    public AccessoryTags currentTag;

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
            slotGameObject.GetComponent<Image>().raycastTarget = true;
            currentTag = currentlyDraggedItem.accessory.accessoryTag;
            currentGameobject = null;
            currentlyDraggedItem = null;
        }
        else
        {
            Debug.Log("Not Good");
        }
    }

    public void OnBeginDrag(PointerEventData data)
    {
        if (slotIsFull)
        {
            slotGameObject.SetActive(true);
            slotIsFull = false;
            currentTag = AccessoryTags.None;    
        }
    }

    public void OnDrag(PointerEventData data)
    {
        slotGameObject.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData data)
    {
        slotGameObject.transform.position = slotGameObject.GetComponent<DragItem>().itemOriginalPosition;
        slotGameObject = null;
        
    }

    public void EmptySlot()
    {
        if (slotIsFull)
        {
            slotGameObject.SetActive(true);
            slotGameObject.transform.position = slotGameObject.GetComponent<DragItem>().itemOriginalPosition;
            slotIsFull = false;
            currentTag = AccessoryTags.None;
        }
    }

}
