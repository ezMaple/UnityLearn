using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DuplicateSampleScript : SampleScript
{
    
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    [Range(1, 50)]
    private int copyCount = 5;

    [SerializeField]
    private float step = 2.0f;

    [SerializeField]
    private float delayBetweenCopies = 0.1f;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    public override void Use()
    {

        StartCoroutine(SpawnCopies());
    }

    
    private IEnumerator SpawnCopies()
    {
        Vector3 startPosition = transform.position;

        for (int i = 1; i <= copyCount; i++)
        {
            Vector3 spawnPosition = startPosition + new Vector3(i * step, 0, 0);
            GameObject clone = Instantiate(prefab, spawnPosition, Quaternion.identity);
            spawnedObjects.Add(clone);

            yield return new WaitForSeconds(delayBetweenCopies);
        }
    }

    
    [ContextMenu("Удалить клонов")]
    public void ClearClones()
    {
        foreach (var obj in spawnedObjects)
        {
            if (obj != null) Destroy(obj);
        }
        spawnedObjects.Clear();
    }
}
