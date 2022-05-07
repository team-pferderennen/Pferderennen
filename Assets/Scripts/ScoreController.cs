using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : GameEventListener
{
    public static int MAX_NUM_OF_POINTS = 10;
    private int actualPoints;
    private int gainedPoints;
    
    private void Start() {
        gainedPoints = 0;
    }

    private void Update()
    {
        if (gainedPoints >= MAX_NUM_OF_POINTS)
            Debug.Log("Next Player!");
    }

    public override void OnEventRaised(int points) {
        gainedPoints += points;
        Debug.Log(gainedPoints);
        //horseController.OnNewPosition(points);
    }
    
}
