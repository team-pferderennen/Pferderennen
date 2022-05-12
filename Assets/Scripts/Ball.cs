using UnityEngine;
using UnityEngine.XR;
using System.Collections;


public class Ball : MonoBehaviour
{  
    private const string TRAJECTORY_PLATE = "trajectoryPlate";
    private const string CHECK_PLATE = "CheckPlate";
    private gameState state = gameState.none;
    private Collider CheckPlateCollider;
    
    private void Start() {
        CheckPlateCollider = GameObject.FindGameObjectWithTag("CheckPlate").GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag(TRAJECTORY_PLATE)) {
            state = gameState.ballIsThrown;
            EventManager.TriggerEvent("ballIsThrown", null);
            CheckPlateCollider.isTrigger = false;
        }
        else if (other.gameObject.CompareTag(CHECK_PLATE) && (state==gameState.ballIsThrown)) {
            state = gameState.ballIsBack;
            EventManager.TriggerEvent("ballIsBack", null);
            CheckPlateCollider.isTrigger = true;
        }
    }
}