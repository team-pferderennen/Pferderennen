using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private int score = 0;
    private int red_points = 5; 
    private int yellow_points = 3; 
    private int green_points = 1;

    private void OnTriggerEnter(Collider other)
    {
        if(this.CompareTag("red_hole")) {
            //this.score += red_points;
            GameEvents.current.HoleEnter(red_points);
        } else if (this.CompareTag("yellow_hole")) {
            //this.score += yellow_points; 
            GameEvents.current.HoleEnter(yellow_points);
        } else if (this.CompareTag("green_hole")) {
            //this.score += green_points;
            GameEvents.current.HoleEnter(green_points);
        } else {
            return; 
        }
    }
}
