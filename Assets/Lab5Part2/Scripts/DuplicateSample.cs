using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Создаёт указанное количество копий объекта через заданный шаг.
/// </summary>
public class DuplicateSampleScript : SampleScript
{
    [Header("Настройки клонирования")]
    [SerializeField, Tooltip("Префаб, который будет копироваться")]
    private GameObject prefab;

    [SerializeField, Tooltip("Количество создаваемых копий")]
    [Range(1, 50)]
    private int copyCount = 5;

    [SerializeField, Tooltip("Шаг между копиями по оси X")]
    private float step = 2.0f;

    [SerializeField, Tooltip("Задержка между созданием копий (сек)")]
    private float delayBetweenCopies = 0.1f;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    public override void Use()
    {
        if (prefab == null)
        {
            Debug.LogError("Prefab не задан! Укажите префаб в инспекторе.", this);
            return;
        }

        StartCoroutine(SpawnCopies());
    }

    /// <summary>
    /// Плавное создание копий с задержкой.
    /// </summary>
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

    /// <summary>
    /// Удаляет все созданные копии.
    /// </summary>
    [ContextMenu("Удалить созданные копии")]
    public void ClearClones()
    {
        foreach (var obj in spawnedObjects)
        {
            if (obj != null) Destroy(obj);
        }
        spawnedObjects.Clear();
    }
}
