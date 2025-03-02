using System.Collections;
using UnityEngine;

public class MoveSample : SampleScript
{
    [SerializeField] private Vector3 targetPosition;
    [SerializeField, Min(0.1f)] private float speed = 1f;

    public override void Use()
    {
        StartCoroutine(MoveToTarget());
    }

    private IEnumerator MoveToTarget()
    {
        Vector3 startPosition = transform.position;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float duration = distance / speed;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }
}