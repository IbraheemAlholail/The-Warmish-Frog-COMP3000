using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class signal : ScriptableObject
{
    public List<signalListener> listeners = new List<signalListener>();

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnSignalRaised();
        }
    }
     public void RegisterListener(signalListener listener)
    {
        listeners.Add(listener);
    }

    public void DeregisterListener(signalListener listener)
    {
        listeners.Remove(listener);
    }
}
