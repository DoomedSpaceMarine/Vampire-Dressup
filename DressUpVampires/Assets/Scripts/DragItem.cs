using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 itemOriginalPosition;

    [SerializeField] private Image itemImage;

    public Accessory accessory;

    private void Start()
    {
        itemOriginalPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData data)
    {
        itemImage.raycastTarget = false;
    }

    public void OnDrag(PointerEventData data)
    {
        transform.position = data.position;
    }

    public void OnEndDrag(PointerEventData data)
    {
        transform.position = itemOriginalPosition;
        itemImage.raycastTarget = true;
    }
    }
