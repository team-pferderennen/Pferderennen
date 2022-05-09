using UnityEngine;


public class HoleController : MonoBehaviour
{
    private const int RED_HOLE_TAG = "red_hole"; 
    private const int YELLOW_HOLE_TAG = "yellow_hole"; 
    private const int GREEN_HOLE_TAG = "green_hole";
    private const int RED_HOLE_POINTS = 3; 
    private const int YELLOW_HOLE_POINTS = 2; 
    private const int GREEN_HOLE_POINTS = 1;
  
    private void OnTriggerEnter(Collider other) {
        int points = GetPoints(other.gameObject.TAG);
        EventManager.TriggerEvent("holeEntered", new Dictionary<string, object>{{"points", points}});
        gameController.eventManager.Raise(points);
    }

    private void GetPoints(string holeType) {
        switch(holeType) {
            case RED_HOLE_TAG: return RED_HOLE_POINTS;
            case YELLOW_HOLE_TAG: return YELLOW_HOLE_POINTS;
            case GREEN_HOLE_TAG: return GREEN_HOLE_POINTS;
        }
    }
}