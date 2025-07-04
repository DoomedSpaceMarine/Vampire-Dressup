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

    private bool itemIsDraggable;

    [SerializeField] private Inventory inventory;

    //Accessory Sprite 
    [SerializeField] private Image headImage;
    [SerializeField] private Image torsoImage;
    [SerializeField] private Image legsImage;
    [SerializeField] private Image feetImage;

    [SerializeField] private Sprite emptySprite;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void OnDrop(PointerEventData data)
    {
        currentGameobject = data.pointerDrag;
        currentlyDraggedItem = currentGameobject.GetComponent<DragItem>();

        if (!slotIsFull && currentGameobject.GetComponent<DragItem>().accessory.accessoryType == targetType)
        {
            Debug.Log("Good");
            currentGameobject.SetActive(false);
            slotIsFull = true;
            slotGameObject = currentGameobject;
            slotGameObject.GetComponent<Image>().raycastTarget = true;
            SetSprite();
            currentTag = currentlyDraggedItem.accessory.accessoryTag;
            currentlyDraggedItem.isInventory = false;
            currentGameobject = null;
            currentlyDraggedItem = null;
            audioManager.SFX_DressClothing();
        }
        else if (slotIsFull && currentGameobject.GetComponent<DragItem>().accessory.accessoryType == targetType)
        {
            //Empty Slot
            slotGameObject.SetActive(true);
            SetSpriteEmpty(slotGameObject.GetComponent<DragItem>().accessory.accessoryType);
            slotIsFull = false;
            currentTag = AccessoryTags.None;
            slotGameObject.transform.localPosition = slotGameObject.GetComponent<DragItem>().itemOriginalPosition;
            slotGameObject.GetComponent<DragItem>().isInventory = true;
            itemIsDraggable = false;
            CheckInventoryStatus();
            slotGameObject = null;

            //Replace new one
            currentGameobject.SetActive(false);
            slotIsFull = true;
            slotGameObject = currentGameobject;
            slotGameObject.GetComponent<Image>().raycastTarget = true;
            SetSprite();
            currentTag = currentlyDraggedItem.accessory.accessoryTag;
            currentlyDraggedItem.isInventory = false;
            currentGameobject = null;
            currentlyDraggedItem = null;
            audioManager.SFX_DressClothing();
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
            SetSpriteEmpty(slotGameObject.GetComponent<DragItem>().accessory.accessoryType);
            slotIsFull = false;
            currentTag = AccessoryTags.None;
            itemIsDraggable = true;
        }
    }

    public void OnDrag(PointerEventData data)
    {
        if (itemIsDraggable)
        {
            slotGameObject.transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData data)
    {
        if (itemIsDraggable)
        {
            slotGameObject.transform.localPosition = slotGameObject.GetComponent<DragItem>().itemOriginalPosition;
            slotGameObject.GetComponent<DragItem>().isInventory = true;
            itemIsDraggable = false;
            CheckInventoryStatus();
            slotGameObject = null;
        }
        
    }

    public void EmptySlot()
    {
        if (slotIsFull)
        {
            slotGameObject.SetActive(true);
            slotGameObject.transform.localPosition = slotGameObject.GetComponent<DragItem>().itemOriginalPosition;
            slotGameObject.GetComponent<DragItem>().isInventory = true;
            SetSpriteEmpty(AccessoryTypes.Head);
            SetSpriteEmpty(AccessoryTypes.Torso);
            SetSpriteEmpty(AccessoryTypes.Legs);
            SetSpriteEmpty(AccessoryTypes.Feet);
            inventory.ShowHeadAccessories();
            slotIsFull = false;
            currentTag = AccessoryTags.None;

        }

        SetBare();
    }

    private void CheckInventoryStatus()
    {
        if(inventory.inventoryState == 0)
        {
            if(slotGameObject.GetComponent<DragItem>().accessory.accessoryType != AccessoryTypes.Head)
            {
                slotGameObject.SetActive(false);
            }
        }
        else if(inventory.inventoryState == 1)
        {
            if (slotGameObject.GetComponent<DragItem>().accessory.accessoryType != AccessoryTypes.Torso)
            {
                slotGameObject.SetActive(false);
            }
        }
        else if(inventory.inventoryState == 2)
        {
            if (slotGameObject.GetComponent<DragItem>().accessory.accessoryType != AccessoryTypes.Legs)
            {
                slotGameObject.SetActive(false);
            }
        }
        else if(inventory.inventoryState == 3)
        {
            if (slotGameObject.GetComponent<DragItem>().accessory.accessoryType != AccessoryTypes.Feet)
            {
                slotGameObject.SetActive(false);
            }
        }
    }

    private void SetSprite()
    {
        switch(currentlyDraggedItem.accessory.accessoryType)
        {
            case AccessoryTypes.Head:
                headImage.sprite = currentlyDraggedItem.accessory.accessorySprite; 
                break;
            case AccessoryTypes.Torso:
                torsoImage.sprite = currentlyDraggedItem.accessory.accessorySprite;
                break;
            case AccessoryTypes.Legs:
                legsImage.sprite = currentlyDraggedItem.accessory.accessorySprite;
                break;
            case AccessoryTypes.Feet:
                feetImage.sprite = currentlyDraggedItem.accessory.accessorySprite;
                break;

        }
    }

    private void SetSpriteEmpty(AccessoryTypes type)
    {
        switch (type)
        {
            case AccessoryTypes.Head:
                headImage.sprite = emptySprite;
                break;

            case AccessoryTypes.Torso:
                torsoImage.sprite = emptySprite;
                break;

            case AccessoryTypes.Legs:
                legsImage.sprite = emptySprite;
                break;

            case AccessoryTypes.Feet:
                feetImage.sprite = emptySprite;
                break;

        }
    }

    private void SetBare()
    {

        if (targetType == AccessoryTypes.Head)
        {
            //Head
            currentGameobject = inventory.head[3].gameObject;
            currentlyDraggedItem = currentGameobject.GetComponent<DragItem>();
            currentGameobject.SetActive(false);
            slotIsFull = true;
            slotGameObject = currentGameobject;
            slotGameObject.GetComponent<Image>().raycastTarget = true;
            SetSprite();
            currentTag = currentlyDraggedItem.accessory.accessoryTag;
            currentlyDraggedItem.isInventory = false;
            currentGameobject = null;
            currentlyDraggedItem = null;
        }

        if (targetType == AccessoryTypes.Legs)
        {
            //Legs
            currentGameobject = inventory.legs[2].gameObject;
            currentlyDraggedItem = currentGameobject.GetComponent<DragItem>();
            currentGameobject.SetActive(false);
            slotIsFull = true;
            slotGameObject = currentGameobject;
            slotGameObject.GetComponent<Image>().raycastTarget = true;
            SetSprite();
            currentTag = currentlyDraggedItem.accessory.accessoryTag;
            currentlyDraggedItem.isInventory = false;
            currentGameobject = null;
            currentlyDraggedItem = null;
        }

        if (targetType == AccessoryTypes.Feet)
        {
            //Feet
            currentGameobject = inventory.feet[2].gameObject;
            currentlyDraggedItem = currentGameobject.GetComponent<DragItem>();
            currentGameobject.SetActive(false);
            slotIsFull = true;
            slotGameObject = currentGameobject;
            slotGameObject.GetComponent<Image>().raycastTarget = true;
            SetSprite();
            currentTag = currentlyDraggedItem.accessory.accessoryTag;
            currentlyDraggedItem.isInventory = false;
            currentGameobject = null;
            currentlyDraggedItem = null;
        }
    }

}
