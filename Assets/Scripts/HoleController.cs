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

    public GameController gameController;

    private void Start() {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
  
    private void OnTriggerEnter(Collider other)
    {
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

    /**
    public void HoleEnter(int points) {
        

        gameController.setScore(points);
        
    }*/

    // private int GetCorrespondingPoints(string holeName) {
    //     if (holeName.Contains("greenHole"))
    //         return Holes.GreenHole;
    //     else if (holeName.Contains("yellowHole"))
    //         return Holes.YellowHole;
    //     else
    //         return Holes.RedHole;
    // }
}