using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IEndDragHandler, IDragHandler
{
    private Vector2 itemOriginalPosition;

    private void Start()
    {
        itemOriginalPosition = transform.position;
    }

    public void OnEndDrag(PointerEventData data)
    {
        transform.position = itemOriginalPosition;
    }

    public void OnDrag(PointerEventData data)
    {
        transform.position = data.position;
    }
}
