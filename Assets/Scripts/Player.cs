using System;
using System.Numerics;

public class Player
{
    private const int POS_DELTA = 3;
    private int playerNr, gainedPoints, numberOfThrownBalls;
    private Vector3 position;
    
    public Player(int playerNr) {
        this.playerNr = playerNr;
        this.gainedPoints = 0;
        this.numberOfThrownBalls = 0;
        Vector3 xrOriginPos = GameObject.FindGameObjectWithTag("XrOrigin").transform.position;
        this.position = GetPlayerPos(playerNr, xrOriginPos);
    }

    private Vector3 GetPlayerPos(int playerNr, Vector3 xrOriginPos) {
        switch(playerNr) {
            case 1: return xrOriginPos;
            case 2: return new Vector3(
                xrOriginPos.x,
                xrOriginPos.y, 
                xrOriginPos.z + POS_DELTA;
            ); 
            case 3: return new Vector3(
                xrOriginPos.x,
                xrOriginPos.y, 
                xrOriginPos.z + 2*POS_DELTA;
            );
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

    public Vector3 Position {
        get { return position; }
        set { position = value; }
    }
}
 