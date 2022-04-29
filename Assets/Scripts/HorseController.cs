using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HorseController : MonoBehaviour
{
    private static float DESIRED_DURATION = 0.5f;
    private float elapsedTime;
    public bool moving;
    private float horseStandLength, stretchPerPoint;
    private Vector3 startPosition, endPosition, 
                    oldPosition, newPosition;
    public GameObject horseStand, gameController;

    void Start() {
        horseStandLength = horseStand.GetComponent<Renderer>().bounds.size.x;
        startPosition = transform.position;
        oldPosition = startPosition;
        newPosition = startPosition;
        endPosition = new Vector3(
            startPosition.x + horseStandLength, 
            startPosition.y, 
            startPosition.z
        );
        stretchPerPoint = horseStandLength/
            gameController.getComponent<GameController>().MAX_NUM_OF_POINTS;
        elapsedTime = 0;
        moving = false;
        GameEvents.current.onHoleEnter += OnNewPosition;
    }

    void FixedUpdate() {
        if ((newPosition.x <= endPosition.x))
            InterpolateHorseMovement();
    }


    private void InterpolateHorseMovement() {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / DESIRED_DURATION;
        transform.position = Vector3.Lerp(
            oldPosition, newPosition, 
            percentageComplete
        );
        if (elapsedTime >= DESIRED_DURATION)
            moving = false;
    }

    public void OnNewPosition() {
        if (moving)
            return;
        int actualPoints = gameController.getComponent<GameController>().actualPoints;
        oldPosition = newPosition;
        float moveDelta = actualPoints*stretchPerPoint;
        Vector3 actualPosition = transform.position;
        newPosition = new Vector3(
            actualPosition.x + moveDelta, 
            actualPosition.y, 
            actualPosition.z
        );
        moving = true;
        elapsedTime = 0;
        if (newPosition.x > endPosition.x)
            newPosition = endPosition;
    }
}
