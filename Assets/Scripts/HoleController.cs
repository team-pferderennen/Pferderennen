using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holes {
    public const int GreenHole = 1, 
    YellowHole = 2, 
    RedHole = 3;
}

public class HoleController : MonoBehaviour
{
    // private static string BALL_TAG = "BallEntered"; 
    private int red_points = 3; 
    private int yellow_points = 2; 
    private int green_points = 1;

    private GameController gameController;

    private void Start() {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
  
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTrigger");
        if(this.CompareTag("red_hole")) {
            //Aufruf des EventManagers
            gameController.eventManager.Raise(red_points);
        } else if (this.CompareTag("yellow_hole")) {
            gameController.eventManager.Raise(yellow_points);
        } else if (this.CompareTag("green_hole"))
        {
            gameController.eventManager.Raise(green_points);
        } else {
            return; 
        }
    }
}