using System.Collections;
using UnityEngine;

public class BounceOnce : MonoBehaviour
{
    [SerializeField]
     float amplitude = 0.5f;
    [SerializeField]
     float duration = 2f;

    Vector3 startPos;

    public void StartBounce()
    {
        startPos = transform.localPosition;
        StartCoroutine(Bounce());
    }

    IEnumerator Bounce()
    {
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float y = Mathf.Abs(Mathf.Sin(t * Mathf.PI * 2)) * amplitude;
            transform.localPosition = startPos + Vector3.up * y;

            yield return null;
        }

        transform.localPosition = startPos; // volver exacto
    }
}
