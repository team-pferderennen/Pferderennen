using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HorseStandManager: GameEventListener {
    private const string HORSE_TAG = "Horse";
    private const int NUM_OF_HORSES = 3;
    private const float DESIRED_INTERP_DURATION = 0.5f;
    private float elapsedTime, stretchPerPoint, 
                  horseObjectLength, horseStandLength;
    private bool newHorsePosIsSet, playerIsChanged;
    private int actualHorseNr;
    private List<GameObject> horses = new List<GameObject>();
    private Vector3 startPos, endPos, 
                    oldPos, newPos;

    public void Start() {
        /********************************
        * TODO: get numOfHorses from the menu system
        ****************************/
        newHorsePosIsSet = false;
        playerIsChanged = false;
        horseStandLength = this.GetComponent<MeshRenderer>().bounds.size.z;
        actualHorseNr = 1;
        stretchPerPoint = horseStandLength/PlayersManager.MAX_NUM_OF_POINTS;
        AddHorses(NUM_OF_HORSES);
        horseObjectLength = horses[actualHorseNr-1].GetComponent<MeshRenderer>().bounds.size.z;
        SetDefaultPositions();
    }

    private void AddHorses(int numOfHorses) {
        for (int horseNr = 1; horseNr <= numOfHorses; horseNr++) {
            string horseTag = HORSE_TAG + horseNr.ToString();
            GameObject newHorseObject = GameObject.FindGameObjectWithTag(horseTag);
            horses.Add(newHorseObject);
        }
    }

    private void SetDefaultPositions() {
        Debug.Log("SetDefaultPositions"+(actualHorseNr-1));
        startPos = horses[actualHorseNr-1].transform.position;
        newPos = startPos; 
        oldPos = newPos;
        endPos = new Vector3(
            startPos.x, 
            startPos.y, 
            startPos.z - horseStandLength + horseObjectLength/2
        );
    }

    private void Update() {
        if (playerIsChanged && !newHorsePosIsSet) {
            Debug.Log("OnPlayerChanged");
            actualHorseNr++;
            SetDefaultPositions();
            playerIsChanged = false;
        }
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
        EventManager.StartListening("holeEntered", this.OnHoleEntered);
        EventManager.StartListening("playerChanged", this.OnPlayerChanged);
    }

    public override void OnDisable() {
        EventManager.StopListening("holeEntered", this.OnHoleEntered);
        EventManager.StopListening("playerChanged", this.OnPlayerChanged);
    }

    private void OnHoleEntered(Dictionary<string, object> message) {
        int points = (int)message["points"];
        Debug.Log("HoleEntered" + points);
        SetNewPos(points);
        newHorsePosIsSet = true;
        elapsedTime = 0;
    }

    private void OnPlayerChanged(Dictionary<string, object> message) {
        playerIsChanged = true;
    }

    public void InterpolateMove(float interpolationRatio) {
        Debug.Log("InterpolateMove"+(actualHorseNr-1));
        horses[actualHorseNr-1].transform.position = Vector3.Lerp(
            oldPos, newPos, 
            interpolationRatio
        );
    }

    public void SetNewPos(int points) {
        oldPos = newPos;
        float moveDelta = (float)(points*stretchPerPoint);
        Vector3 actualPos = horses[actualHorseNr-1].transform.position;
        newPos = new Vector3(
            actualPos.x, 
            actualPos.y, 
            actualPos.z - moveDelta
        );
        if (newPos.z <= endPos.z)
            newPos = endPos;
    }
}