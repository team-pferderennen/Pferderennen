using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEventListener: MonoBehaviour
{
    public abstract void OnEnable();
    public abstract void OnEventRaised(int points);
}