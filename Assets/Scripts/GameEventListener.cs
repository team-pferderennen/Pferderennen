namespace DefaultNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class GameEventListener: MonoBehaviour
{
    public abstract OnEnable();
    public abstract OnEventRaised();
}