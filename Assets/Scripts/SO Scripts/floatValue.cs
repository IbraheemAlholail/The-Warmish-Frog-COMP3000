using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class floatValue : ScriptableObject, ISerializationCallbackReceiver
{
    public float initialVal;
    [HideInInspector] public float Runtimeval;

    public void OnAfterDeserialize()
    {
        Runtimeval = initialVal;
    }
    public void OnBeforeSerialize()
    {

    }
   
}
