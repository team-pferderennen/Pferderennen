using UnityEngine;
using System.Collections.Generic;


public class HoleController : MonoBehaviour
{
    private const string RED_HOLE_TAG = "redHole";
    private const string YELLOW_HOLE_TAG = "yellowHole";
    private const string GREEN_HOLE_TAG = "greenHole";
    private const int RED_HOLE_POINTS = 3; 
    private const int YELLOW_HOLE_POINTS = 2; 
    private const int GREEN_HOLE_POINTS = 1;
    private const int ZERO_POINTS = 0;
    
  
    private void OnTriggerEnter(Collider other) {
        int points = GetPoints(this.tag);
        Debug.Log(points);
        EventManager.TriggerEvent("holeEntered", new Dictionary<string, object>{{"points", points}});
    }

    private int GetPoints(string holeTag) {
        switch(holeTag) {
            case RED_HOLE_TAG: return RED_HOLE_POINTS;
            case YELLOW_HOLE_TAG: return YELLOW_HOLE_POINTS;
            case GREEN_HOLE_TAG: return GREEN_HOLE_POINTS;
            default: return ZERO_POINTS;
        }
    }
}