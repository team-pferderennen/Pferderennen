// Playermanager

/// Collider für Würfe, die daneben gegangen sind.

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using UnityEngine;

public class PlayersManager: GameEventListener
{
    public const int MAX_NUM_OF_POINTS = 10;
    private const int NUM_OF_PLAYERS = 3;
    private const int MAX_NUM_OF_THROWN_BALLS = 10;
    private int numOfPlayers;
    private int actualPlayerNr;
    private bool playersAreReady;
    private List<Player> players = new List<Player>();


    private void Start() {
        playersAreReady = false;
        /********************************
        * TODO: get numOfPlayers from the menu system
        ****************************/
        actualPlayerNr = 1;
        UnityEngine.Vector3 unityStartPos = transform.position;
        System.Numerics.Vector3 startPos = new System.Numerics.Vector3(
            unityStartPos.x, unityStartPos.y, unityStartPos.z);
        AddPlayers(NUM_OF_PLAYERS, startPos);
    }

    private void Update() {
        if (FinishedGame() && playersAreReady) {
            List<int> winnerPlayersNrs = GetWinner(); 
            /********************************
            * TODO: show the winners on the board for a while (EventManager)
            * TODO: switch to game menu
            ****************************/
            Debug.Log(winnerPlayersNrs);
        }
        if (PlayerCanBeSwitched() && playersAreReady) {
            Debug.Log("PlayerSwitched");
            SwitchToNextPlayer(); 
            ChangeXrOriginPos();
            EventManager.TriggerEvent("playerChanged", null);
        }
    }

    public void AddPlayers(int numOfPlayers, System.Numerics.Vector3 startPos) {
        for (int playerNr = 0; playerNr < numOfPlayers; playerNr++) {
            Player newPlayer = new Player(playerNr, startPos);
            players.Add(newPlayer);
        }
        playersAreReady = true;
    }


    public override void OnEnable() {
        EventManager.StartListening("holeEntered", this.OnHoleEntered);
        EventManager.StartListening("ballIsThrown", this.OnBallThrown);
    }

    public override void OnDisable() {
        EventManager.StopListening("holeEntered", this.OnHoleEntered);
        EventManager.StopListening("ballIsThrown", this.OnBallThrown);
    }

    private void OnHoleEntered(Dictionary<string, object> message) {
        int points = (int)message["points"];
        UpdateGainedPoints(points);
    }

    private void OnBallThrown(Dictionary<string, object> message) {
        Debug.Log("BallEventFromPlayersManager");
        IncreaseThrownBallsNr();
    }

    public void ChangeXrOriginPos() {
        System.Numerics.Vector3 actualPos = ActualPlayerPos();
        transform.position = new UnityEngine.Vector3(actualPos.X, actualPos.Y, actualPos.Z);
    }

    private bool PlayerCanBeSwitched() {
        return ((players[actualPlayerNr-1].GainedPoints >= MAX_NUM_OF_POINTS) ||
                (players[actualPlayerNr-1].NumberOfThrownBalls >= MAX_NUM_OF_THROWN_BALLS));
    } 

    private void SwitchToNextPlayer() {
        if (actualPlayerNr < NUM_OF_PLAYERS) {
            players[actualPlayerNr-1].CalculateTotalScore();
            actualPlayerNr++;
        }
    }

    private void UpdateGainedPoints(int points) {
        players[actualPlayerNr-1].GainedPoints += points;
    }

    private int GainedPoints() {
        return players[actualPlayerNr-1].GainedPoints;
    }

    private void IncreaseThrownBallsNr() {
        players[actualPlayerNr-1].NumberOfThrownBalls++;
    }

    private System.Numerics.Vector3 ActualPlayerPos() {
        return players[actualPlayerNr-1].position;
    }

    private bool FinishedGame() {
        if (actualPlayerNr > numOfPlayers)
            return true;
        return false;
    }


    private List<int> GetWinner() {
        List<int> winnerPlayersNrs = new List<int>();
        int maxGainedPoints = players.Max(player => player.TotalScore);
        /* 
        List<int> gainedPoints = new List<int>;
        for (playerNr = 0; playerNr < numOfPlayers; playerNr++)
            gainedPoints.Add(players[playerNr].GainedPoints)
        int maxGainedPoints = gainedPoints.Max(); */
        for (int playerNr = 0; playerNr < numOfPlayers; playerNr++) {
            if(maxGainedPoints == players[playerNr].TotalScore)
                winnerPlayersNrs.Add(playerNr);
        }
        return winnerPlayersNrs;
    }

    public int NumOfPlayers {
        get { return numOfPlayers; }
        set { numOfPlayers = value; }
    }

    public int ActualPlayerNr {
        get { return actualPlayerNr; }
        set { actualPlayerNr = value; }
    }
}