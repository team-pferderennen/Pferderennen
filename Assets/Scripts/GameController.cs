using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static int MAX_NUM_OF_POINTS = 20;
    public bool newHoleEnter;
    public int actualPoints;
    private int gainedPoints;

    private void Start() {
        newHoleEnter = false;
        gainedPoints = 0;
    }

    private void Update() {
        if (newHoleEnter) {
            gainedPoints += actualPoints;
            newHoleEnter = false;
        }
        if (gainedPoints >= MAX_NUM_OF_POINTS)
            Debug.Log("Next Player!");
    }
}
