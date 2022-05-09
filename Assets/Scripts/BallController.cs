using UnityEngine;


public class BallController : MonoBehaviour
{  
    private void OnTriggerEnter(Collider other) {
        EventManager.TriggerEvent("ballIsThrown", null);
    }
}