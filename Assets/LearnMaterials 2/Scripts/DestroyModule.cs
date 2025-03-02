using System.Collections;
using UnityEngine;

[HelpURL("https://docs.google.com/document/d/1RMamVxE-yUpSfsPD_dEa4-Ak1qu6NTo83qY1O4XLxUY/edit?usp=sharing")]
public class DestroyModule : MonoBehaviour
{   
    [Tooltip("Задержка перед уничтожением следующего объекта")]
    [SerializeField, Min(0)]
    private float destroyDelay;
    [SerializeField, Min(0)]
    private int minimalDestroyingObjectsCount;

    private Transform myTransform => transform;
    
    [ContextMenu("Начать удаление объектов")]
    public void ActivateModule()
    {
        StartCoroutine(DestroyRandomChildObjectCoroutine());
    }
    
    private IEnumerator DestroyRandomChildObjectCoroutine()
    {
        while (myTransform.childCount > minimalDestroyingObjectsCount)
        {
            int index = Random.Range(0, myTransform.childCount - 1);
            Destroy(myTransform.GetChild(index).gameObject);
            yield return new WaitForSeconds(destroyDelay);
        }
        Destroy(gameObject, Time.deltaTime);
    }
}
