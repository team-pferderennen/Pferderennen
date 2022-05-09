using System;
using System.Numerics;
using UnityEngine;

public class Player
{
    private const int POS_DELTA = 3;
    private int playerNr, gainedPoints, numberOfThrownBalls;
    public System.Numerics.Vector3 position;
    
    public Player(int playerNr) {
        this.playerNr = playerNr;
        this.gainedPoints = 0;
        this.numberOfThrownBalls = 0;
        UnityEngine.Vector3 unityPos = GameObject.FindGameObjectWithTag("XROrigin").transform.position;
        System.Numerics.Vector3 xrOriginPos = new System.Numerics.Vector3(unityPos.x, unityPos.y, unityPos.z);
        this.position = GetPlayerPos(playerNr, xrOriginPos);
    }

    private System.Numerics.Vector3 GetPlayerPos(int playerNr, System.Numerics.Vector3 xrOriginPos) {
        switch(playerNr) {
            case 1: return xrOriginPos;
            case 2: return new System.Numerics.Vector3(
                xrOriginPos.X,
                xrOriginPos.Y,
                xrOriginPos.Z + POS_DELTA
            ); 
            case 3: return new System.Numerics.Vector3(
                xrOriginPos.X,
                xrOriginPos.Y,
                xrOriginPos.Z + 2*POS_DELTA
            );
            default:
                 return xrOriginPos;
        }
    }

    public int PlayerNr {
        get { return playerNr; }
        set { playerNr = value; }
    }

    public int GainedPoints {
        get { return gainedPoints; }
        set { gainedPoints = value; }
    }

    public int NumberOfThrownBalls {
        get { return numberOfThrownBalls; }
        set { numberOfThrownBalls = value; }
    }

    public System.Numerics.Vector3 Position {
        get { return position; }
        set { position = value; }
    }
}
 