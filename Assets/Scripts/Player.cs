using System;
using System.Numerics;

public class Player
{
    private const float POS_DELTA = 2.7f;
    private int playerNr, gainedPoints, totalScore, numberOfThrownBalls;
    public Vector3 position;
    
    public Player(int playerNr, Vector3 startPos) {
        this.playerNr = playerNr;
        this.gainedPoints = 0;
        this.numberOfThrownBalls = 0;
        this.totalScore = 0;
        this.position = GetPlayerPos(playerNr, startPos);
    }

    private System.Numerics.Vector3 GetPlayerPos(int playerNr, Vector3 startPos) {
        switch(playerNr) {
            case 2: return new Vector3(
                startPos.X,
                startPos.Y,
                startPos.Z - POS_DELTA
            ); 
            case 3: return new Vector3(
                startPos.X,
                startPos.Y,
                startPos.Z - 2*POS_DELTA
            );
            default:
                 return startPos;
        }
    }

    public void CalculateTotalScore() {
        totalScore = (int)(10 * gainedPoints / numberOfThrownBalls);
    }

    public int PlayerNr {
        get { return playerNr; }
        set { playerNr = value; }
    }

    public int GainedPoints {
        get { return gainedPoints; }
        set { gainedPoints = value; }
    }

    public int TotalScore {
        get { return totalScore; }
        set { totalScore = value; }
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
 