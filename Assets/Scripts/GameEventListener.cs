using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEventListener: MonoBehaviour
{
    public void OnEventEnable(EventManager eventManager) {
        eventManager.RegisterListener(this);
    }

    public void OnEventDisable(EventManager eventManager) {
        eventManager.UnregisterListener(this);
    }
    
    public abstract void OnEventRaised(int points);
}