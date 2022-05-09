using UnityEngine;
using System.Collections.Generic;


public class HoleController : MonoBehaviour
{
    private const string RED_HOLE_TAG = "red_hole";
    private const string YELLOW_HOLE_TAG = "yellow_hole";
    private const string GREEN_HOLE_TAG = "green_hole";
    private const int RED_HOLE_POINTS = 3; 
    private const int YELLOW_HOLE_POINTS = 2; 
    private const int GREEN_HOLE_POINTS = 1;
    private const int ZERO_POINTS = 0;
    
  
    private void OnTriggerEnter(Collider other) {
        int points = GetPoints(other.gameObject.tag);
        EventManager.TriggerEvent("holeEntered", new Dictionary<string, object>{{"points", points}});
    }

    private int GetPoints(string holeType) {
        switch(holeType) {
            case RED_HOLE_TAG: return RED_HOLE_POINTS;
            case YELLOW_HOLE_TAG: return YELLOW_HOLE_POINTS;
            case GREEN_HOLE_TAG: return GREEN_HOLE_POINTS;
            default: return ZERO_POINTS;
        }
    }
}