using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUISnapCenter : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    public RectTransform bounds;
    public float snapRadius = 20f; // tolerancia en píxeles
    RectTransform rect;
    RectTransform canvasRect;
    Canvas canvas;
    Vector2 offset;
    public UISnapPoint currentSnap;

    void Awake()
    {
        rect = (RectTransform)transform;
        canvas = GetComponentInParent<Canvas>();
        canvasRect = canvas.transform as RectTransform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (currentSnap != null)
        {
            currentSnap.Vacate();
            currentSnap = null;
        }

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect, eventData.position, eventData.pressEventCamera, out var canvasPoint);

        offset = rect.anchoredPosition - canvasPoint;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect, eventData.position, eventData.pressEventCamera, out var canvasPoint);

        Vector2 pos = canvasPoint + offset;
        ClampToBoundsCanvas(ref pos);
        rect.anchoredPosition = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!TrySnapToCenter())
        {
            LeanTween.moveLocal(gameObject, Vector3.zero, 0.1f);
        }
    }

    bool TrySnapToCenter()
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
            // Vector2 canvasPos = BestToCanvasLocal(best.rect);
            // rect.anchoredPosition = canvasPos;

            SnapToPoint(best);

            return true;
        }
        return false;

    }
    public void SnapToPoint(UISnapPoint uISnap)
    {
        rect.position = uISnap.rect.TransformPoint(uISnap.rect.rect.center);
        uISnap.Occupy(GetComponent<WordUI>().word ?? string.Empty);
        currentSnap = uISnap;
    }
    Vector2 CanvasToParentLocal(Vector2 canvasPos, RectTransform parent)
    {
        Vector3 world = canvasRect.TransformPoint(canvasPos);
        return parent.InverseTransformPoint(world);
    }

    Vector2 BestToCanvasLocal(RectTransform best)
    {
        Vector3 worldPos = best.TransformPoint(best.rect.center);
        return canvasRect.InverseTransformPoint(worldPos);
    }

    Vector2 GetWorldCenter(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);
        return (corners[0] + corners[2]) * 0.5f;
    }

    void ClampToBounds(ref Vector2 pos)
    {
        Vector2 min = (bounds.rect.min - rect.rect.min);
        Vector2 max = bounds.rect.max - rect.rect.max;

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);
    }
    void ClampToBoundsCanvas(ref Vector2 pos)
    {
        Vector3[] b = new Vector3[4];
        bounds.GetWorldCorners(b);

        Vector3[] r = new Vector3[4];
        rect.GetWorldCorners(r);

        // Convertir corners a Canvas local space
        Vector2 bMin = canvasRect.InverseTransformPoint(b[0]);
        Vector2 bMax = canvasRect.InverseTransformPoint(b[2]);

        Vector2 rMin = canvasRect.InverseTransformPoint(r[0]);
        Vector2 rMax = canvasRect.InverseTransformPoint(r[2]);

        Vector2 min = bMin - (rMin - rect.anchoredPosition);
        Vector2 max = bMax - (rMax - rect.anchoredPosition);

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);
    }


}
