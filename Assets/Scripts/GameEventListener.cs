using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEventListener: MonoBehaviour
{
    public abstract void OnEventEnable(EventManager eventManager);
    public abstract void OnEventDisable(EventManager eventManager);
    public abstract void OnEventRaised(int points);
}