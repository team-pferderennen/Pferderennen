using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EventManager", menuName = "horse-race/EventManager", order = 0)]

public class EventManager : ScriptableObject {
    private List<GameEventListener> listeners = new List<GameEventListener>();
    
    public void Raise(int points)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(points); 
    }

    public void RegisterListener(GameEventListener listener) {
        listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener) {
        listeners.Remove(listener);
    }
    
}
