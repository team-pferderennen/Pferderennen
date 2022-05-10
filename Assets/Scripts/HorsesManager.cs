using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HorseStandManager: GameEventListener {
    private const string HORSE_TAG = "Horse";
    private const string HORSE_STAND_TAG = "HorseStand";
    private const float NUM_OF_HORSES = 3;
    private const float DESIRED_INTERP_DURATION = 0.5f;
    private float elapsedTime, stretchPerPoint, 
                  horseObjectLength, horseStandLength;
    private bool newHorsePosIsSet;
    private int actualHorseNr;
    private List<GameObject> horses = new List<GameObject>();
    private Vector3 startPos, endPos, 
                    oldPos, newPos;

    public void Start() {
        /********************************
        * TODO: get numOfHorses from the menu system
        ****************************/
        horseStandLength = this.GetComponent<MeshRenderer>().bounds.size.z;
        actualHorseNr = 1;
        horseObjectLength = horses[actualHorseNr].GetComponent<MeshRenderer>().bounds.size.z;
        stretchPerPoint = horseStandObjectLength/PlayersManager.MAX_NUM_OF_POINTS;
        AddHorses(NUM_OF_HORSES);
        SetDefaultPositions();
        OnEnable();
    }

    private void AddHorses(int numOfHorses, float horseStandLength) {
        for (int horseNr = 0; horseNr < numOfHorses; horseNr++) {
            string horseTag = HORSE_TAG + horseNr.ToString();
            newHorseObject = GameObject.FindGameObjectWithTag(horseTag);
            horses.Add(newHorseObject);
        }
    }

    private void SetDefaultPositions() {
        startPos = horses[actualHorseNr].transform.position;
        newPos = startPos; 
        oldPos = newPos;
        endPos = new Vector3(
            startPos.x, 
            startPos.y, 
            startPos.z - horseStandObjectLength + horseObjectLength/2
        );
    }

    public void FixedUpdate() {
        if (newHorsePosIsSet) {
            float interpolationRatio = elapsedTime / DESIRED_INTERP_DURATION;
            InterpolateMove(interpolationRatio);
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= DESIRED_INTERP_DURATION)
                newHorsePosIsSet = false;
        }
    }

    public override void OnEnable() {
        EventManager.StartListening("holeEntered", OnHoleEntered);
        EventManager.StartListening("playerChanged", OnPlayerChanged);
    }

    public override void OnDisable() {
        EventManager.StopListening("holeEntered", OnHoleEntered);
        EventManager.StopListening("playerChanged", OnPlayerChanged);
    }

    private void OnHoleEntered(Dictionary<string, object> message) {
        int points = (int)message["points"];
        SetNewPos(points);
        newHorsePosIsSet = true;
        elapsedTime = 0;
    }

    private void OnPlayerChanged(Dictionary<string, object> message) {
        actualHorseNr++;
        SetDefaultPositions();
    }

    public void InterpolateMove(float interpolationRatio) {
        horses[actualHorseNr].transform.position = Vector3.Lerp(
            oldPos, newPos, 
            interpolationRatio
        );
    }

    public void SetNewPos(int points) {
        oldPos = newPos;
        float moveDelta = (float)(points*stretchPerPoint);
        Vector3 actualPos = horses[actualHorseNr].transform.position;
        newPos = new Vector3(
            actualPos.x, 
            actualPos.y, 
            actualPos.z - moveDelta
        );
        if (newPos.z <= endPos.z)
            newPos = endPos;
    }
}