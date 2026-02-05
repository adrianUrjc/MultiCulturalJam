using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUISnapCenter : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    public RectTransform bounds;
    public float snapRadius = 20f; // tolerancia en píxeles

    RectTransform rect;
    Vector2 offset;
    UISnapPoint currentSnap;

    void Awake()
    {
        rect = (RectTransform)transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // liberar slot anterior
        if (currentSnap != null)
        {
            currentSnap.occupied = false;
            currentSnap = null;
        }

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rect, eventData.position, eventData.pressEventCamera, out offset);
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            bounds, eventData.position, eventData.pressEventCamera, out var localPoint);

        Vector2 pos = localPoint - offset;
        ClampToBounds(ref pos);
        rect.anchoredPosition = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        TrySnapToCenter();
    }

    // -------------------------------------------------------

    void TrySnapToCenter()
    {
        Vector2 myCenter = GetWorldCenter(rect);

        UISnapPoint best = null;
        float bestDist = float.MaxValue;

        foreach (var snap in FindObjectsByType<UISnapPoint>(FindObjectsSortMode.None))
        {
            if (snap.occupied) continue;

            Vector2 snapCenter = GetWorldCenter(snap.rect);
            float dist = Vector2.Distance(myCenter, snapCenter);

            if (dist < snapRadius && dist < bestDist)
            {
                best = snap;
                bestDist = dist;
            }
        }

        if (best != null)
        {
            // SNAP EXACTO AL CENTRO
            rect.position = best.rect.position;
            best.occupied = true;
            currentSnap = best;
        }
    }

    Vector2 GetWorldCenter(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        return (corners[0] + corners[2]) * 0.5f;
    }

    void ClampToBounds(ref Vector2 pos)
    {
        Vector2 min = bounds.rect.min - rect.rect.min;
        Vector2 max = bounds.rect.max - rect.rect.max;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);
    }
}
