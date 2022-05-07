using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HorseController : GameEventListener
{
    private static float DESIRED_DURATION = 0.5f;
    private float elapsedTime;
    public bool moving;
    private float horseStandLength, stretchPerPoint, horseLength;
    private Vector3 startPosition, endPosition, 
                    oldPosition, newPosition;
    public GameObject horseStand;
    private GameController gameController;

    public override void OnEventEnable(EventManager eventManager) {
        eventManager.RegisterListener(this.GetComponent<GameEventListener>());
    }

    public override void OnEventDisable(EventManager eventManager) {
        eventManager.UnregisterListener(this.GetComponent<GameEventListener>());
    }

    private void Start() {
        horseStandLength = horseStand.GetComponent<MeshRenderer>().bounds.size.z;
        horseLength = this.GetComponent<MeshRenderer>().bounds.size.z;
        //Debug.Log("MeshSize: " + horseStand.GetComponent<MeshRenderer>().bounds.size);
        //Debug.Log("horseStandLength: " + horseStandLength);
        startPosition = transform.position;
        //Debug.Log("startPosition: " + startPosition);
        oldPosition = startPosition;
        newPosition = startPosition;
        endPosition = new Vector3(
            startPosition.x, 
            startPosition.y, 
            startPosition.z - horseStandLength + horseLength
        );
        //Debug.Log("endPosition: " + endPosition);
        stretchPerPoint = horseStandLength/
            ScoreController.MAX_NUM_OF_POINTS;
        //Debug.Log("stretchPerPoint: " + stretchPerPoint);
        elapsedTime = 0;
        moving = false;
        // GameEvents.current.onHoleEnter += OnNewPosition;
    }

    void FixedUpdate() {
        if ((newPosition.z >= endPosition.z))
            InterpolateHorseMovement();
    }


    private void InterpolateHorseMovement() {
        float interpolationRatio = elapsedTime / DESIRED_DURATION;
        transform.position = Vector3.Lerp(
            oldPosition, newPosition, 
            interpolationRatio
        );
        //Debug.Log("NewPosition: " + newPosition);
        elapsedTime += Time.deltaTime;
        //Debug.Log("interpolationRatio: " + interpolationRatio);
        if (elapsedTime <= DESIRED_DURATION)
            moving = false;
    }

    public override void OnEventRaised(int points) {
        if (moving)
            return;
        //int actualPoints = gameController.GetComponent<GameController>().actualPoints;
        oldPosition = newPosition;
        float moveDelta = (float)(points*stretchPerPoint);
        //Debug.Log("moveDelta: " + moveDelta);
        Vector3 actualPosition = transform.position;
        newPosition = new Vector3(
            actualPosition.x, 
            actualPosition.y, 
            actualPosition.z - moveDelta
        );
        //Debug.Log("ActualPosition: " + actualPosition);
        moving = true;
        elapsedTime = 0;
        if (newPosition.z < endPosition.z)
            newPosition = endPosition;
    }
}
