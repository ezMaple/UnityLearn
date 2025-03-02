using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchrinkAndDestroy : SampleScript
{

    [SerializeField] private Transform target;
    [SerializeField, Min(0.1f)] private float shrinkDuration = 1f;
    public override void Use(){

        StartCoroutine(ShrinkAndDestroy());

    }

    private IEnumerator ShrinkAndDestroy(){

         foreach (Transform child in target)
        {
            StartCoroutine(ShrinkAndDestroyObject(child));
        }

        yield return new WaitForSeconds(shrinkDuration);

        foreach (Transform child in target)
        {
            Destroy(child.gameObject);
        }
    }

    private IEnumerator ShrinkAndDestroyObject(Transform obj)
    {
        Vector3 originalScale = obj.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < shrinkDuration)
        {
            obj.localScale = Vector3.Lerp(originalScale, Vector3.zero, elapsedTime / shrinkDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // obj.localScale = Vector3.zero;
    }



}
