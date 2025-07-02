using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Vector2 itemOriginalPosition;

    [SerializeField] private Image itemImage;

    public Accessory accessory;

    public bool isInventory = true;

    private bool hasPositionBeenSet;

    private RectTransform transformRect;

    private void Awake()
    {
        transformRect = GetComponent<RectTransform>();
        itemOriginalPosition = transform.localPosition;
    }

    private void Start()
    {
        isInventory = true;
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
        transform.localPosition = itemOriginalPosition;
        itemImage.raycastTarget = true;
    }
    }
