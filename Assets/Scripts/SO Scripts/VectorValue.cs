using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver
{
    public Vector2 initVal;
    public Vector2 defaultVal;
    public void OnAfterDeserialize()
    {
        initVal = defaultVal;
    }
    public void OnBeforeSerialize()
    {
    }

}
