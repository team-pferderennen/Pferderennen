using UnityEngine;


public class Ball : MonoBehaviour
{  
    private const string TRAJECTORY_PLATE = "trajectoryPlate";

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag(TRAJECTORY_PLATE))
            EventManager.TriggerEvent("ballIsThrown", null);
    }
}