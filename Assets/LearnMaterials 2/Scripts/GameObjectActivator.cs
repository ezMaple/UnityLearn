using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Управляет состоянием (включено/выключено) указанных объектов
/// </summary>
[HelpURL("https://docs.google.com/document/d/1GP4_m0MzOF8L5t5pZxLChu3V_TFIq1czi1oJQ2X5kpU/edit?usp=sharing")]
public class GameObjectActivator : MonoBehaviour
{
    [Header("Настройки модуля")]
    [SerializeField, Tooltip("Включить отладочный режим (показывает связи в сцене)")]
    private bool debug = false;

    [SerializeField, Tooltip("Объекты, которые будут переключены")]
    private List<StateContainer> targets = new List<StateContainer>();

    private void Awake()
    {
        if (targets == null || targets.Count == 0)
        {
            Debug.LogWarning("Список targets пуст! Объекты не будут управляться.", this);
            return;
        }

        foreach (var item in targets)
        {
            if (item.targetGO != null)
            {
                item.defaultValue = item.targetGO.activeSelf;
            }
            else
            {
                Debug.LogError("В списке targets есть объект с пустой ссылкой!", this);
            }
        }
    }

    /// <summary>
    /// Переключает состояние всех объектов
    /// </summary>
    [ContextMenu("Переключить объекты")]
    public void ActivateModule()
    {
        SetStateForAll();
    }

    /// <summary>
    /// Возвращает объекты в их изначальное состояние
    /// </summary>
    [ContextMenu("Переключить объекты в состояние по умолчанию")]
    public void ReturnToDefaultState()
    {
        foreach (var item in targets)
        {
            if (item.targetGO != null)
            {
                item.targetGO.SetActive(item.defaultValue);
                item.targetState = item.defaultValue;
            }
        }
    }

    private void SetStateForAll()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] != null && targets[i].targetGO != null)
            {
                targets[i].targetGO.SetActive(targets[i].targetState);
                targets[i].targetState = !targets[i].targetState;
            }
            else
            {
                Debug.LogError($"Элемент {i} равен null. Вероятно, была утеряна ссылка. Источник: {gameObject.name}", this);
            }
        }
    }

    #region Гизмо для отладки
    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawSphere(transform.position, 0.3f);

            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i] != null && targets[i].targetGO != null)
                {
                    Gizmos.color = targets[i].targetState ? Color.green : Color.red;
                    Gizmos.DrawLine(transform.position, targets[i].targetGO.transform.position);
                }
            }
        }
    }
    #endregion
}

[System.Serializable]
public class StateContainer
{
    [Tooltip("Объект, которому нужно задать состояние")]
    public GameObject targetGO;

    [Tooltip("Целевое состояние. Если отмечено, объект будет включен")]
    public bool targetState = false;

    [HideInInspector] public bool defaultValue;
}
