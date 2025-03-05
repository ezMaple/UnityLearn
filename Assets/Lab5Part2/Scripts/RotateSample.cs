using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSample : SampleScript
{   
    [SerializeField]
    public Vector3 rotationAngles;
    [SerializeField]
    public float rotationSpeed = 10f;
    
    public override void Use()
    {
        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles + rotationAngles);
        float duration = rotationAngles.magnitude / rotationSpeed;
        float elapsedTime = 0;
        
        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        transform.rotation = targetRotation;
    }
}
