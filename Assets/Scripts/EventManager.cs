using System.Collections;
using System.Collections.Generic;

public class EventManager
{
    private List<GameEventListener> listeners = new List<GameEventListener>();
    
    public void Raise(int points)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(points); 
    }

    public void RegisterListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }
    
}
