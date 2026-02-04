using System.Collections;
using UnityEngine;

public class SideToSideOnce : MonoBehaviour
{
    [SerializeField] float distance = 1f;
    [SerializeField] float duration = 2f;

    Vector3 startPos;

    public void StartSide()
    {
        startPos = transform.localPosition;
        StartCoroutine(Side());
    }

    IEnumerator Side()
    {
        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            float x = Mathf.Sin(t * Mathf.PI * 2) * distance;
            transform.localPosition = startPos + Vector3.right * x;

            yield return null;
        }

        transform.localPosition = startPos;
    }
}
