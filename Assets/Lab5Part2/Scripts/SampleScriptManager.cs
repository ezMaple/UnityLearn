using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

  private List<SampleScript> sampleScripts = new List<SampleScript>();

    private void Awake()
    {
        sampleScripts.AddRange(FindObjectsOfType<SampleScript>());
    }

    [ContextMenu("Запустить все")]
    public void UseAll()
    {
        foreach (SampleScript script in sampleScripts)
        {
            script.Use();
        }
    }
    
}
