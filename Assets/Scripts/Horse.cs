using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Horse
{
    private const string HORSE_TAG = "Horse";
    private const string HORSE_STAND_TAG = "HorseStand";
    private int horseNr;
    private float stretchPerPoint;
    private GameObject horseObject, horseStandObject;
    private Vector3 startPos, endPos, 
                    oldPos, newPos;
    
    public Horse(int horseNr) {
        this.horseNr = horseNr;
        string horseTag = HORSE_TAG + horseNr.ToString();
        this.horseObject = GameObject.FindGameObjectWithTag(horseTag);
        this.horseStandObject = GameObject.FindGameObjectWithTag(HORSE_STAND_TAG);
        float horseObjectLength = horseObject.GetComponent<MeshRenderer>().bounds.size.z;
        float horseStandObjectLength = horseStandObject.GetComponent<MeshRenderer>().bounds.size.z;
        this.startPos = horseObject.transform.position;
        this.newPos = startPos; 
        this.oldPos = newPos;
        this.endPos = new Vector3(
            startPos.x, 
            startPos.y, 
            startPos.z - horseStandObjectLength + horseObjectLength/2
        );
        this.stretchPerPoint = horseStandObjectLength/PlayersManager.MAX_NUM_OF_POINTS;
    }

    public void InterpolateMove(float interpolationRatio) {
        horseObject.transform.position = Vector3.Lerp(
            oldPos, newPos, 
            interpolationRatio
        );
    }

    public void SetNewPos(int points) {
        oldPos = newPos;
        float moveDelta = (float)(points*stretchPerPoint);
        Vector3 actualPos = horseObject.transform.position;
        newPos = new Vector3(
            actualPos.x, 
            actualPos.y, 
            actualPos.z - moveDelta
        );
        if (newPos.z <= endPos.z)
            newPos = endPos;
    }

    public int HorseNr {
        get { return horseNr; }
        set { horseNr = value; }
    }

    public Vector3 StartPos {
        get { return startPos; }
        set { startPos = value; }
    }

    public Vector3 EndPos {
        get { return endPos; }
        set { endPos = value; }
    }

    public Vector3 NewPos {
        get { return newPos; }
        set { newPos = value; }
    }
} 