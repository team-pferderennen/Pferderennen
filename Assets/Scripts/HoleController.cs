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
    private static string BALL_TAG = "BallEntered";
    public GameObject gameController;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag(BALL_TAG)) {
            gameController.GetComponent<GameController>().newHoleEnter = true;
            gameController.GetComponent<GameController>().actualPoints = 
                GetCorrespondingPoints(this.name);
            GameEvents.current.HoleEnter();
        }
    }

    private int GetCorrespondingPoints(string holeName) {
        if (holeName.Contains("greenHole"))
            return Holes.GreenHole;
        else if (holeName.Contains("yellowHole"))
            return Holes.YellowHole;
        else
            return Holes.RedHole;
    }
}