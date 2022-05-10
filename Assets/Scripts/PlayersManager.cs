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
    private const int MAX_NUM_OF_PLAYERS = 3;
    private const int MAX_NUM_OF_THROWN_BALLS = 5;
    private List<Player> players = new List<Player>();
    private int numOfPlayers;
    private int actualPlayerNr;

    private void Start() {
        /********************************
        * TODO: get numOfPlayers from the menu system
        ****************************/
        actualPlayerNr = 1;
        UnityEngine.Vector3 unityStartPos = transform.position;
        System.Numerics.Vector3 startPos = new System.Numerics.Vector3(
            unityStartPos.x, unityStartPos.y, unityStartPos.z);
        AddPlayers(numOfPlayers=1, startPos);
        OnEnable();
    }

    private void Update() {
        if (FinishedGame()) {
            List<int> winnerPlayersNrs = playerManager.GetWinner(); 
            /********************************
            * TODO: show the winners on the board for a while
            * TODO: switch to game menu
            ****************************/
            Debug.Log(winnerPlayersNrs);
        }
        if (PlayerCanBeSwitched()) {
            ChangeXrOriginPos();
            EventManager.TriggerEvent("playerChanged", null);
        }
    }

    public void AddPlayers(int numOfPlayers, Vector3 startPos) {
        for (int playerNr = 0; playerNr < numOfPlayers; playerNr++) {
            Player newPlayer = new Player(playerNr, startPos);
            players.Add(newPlayer);
        }
    }


    public override void OnEnable() {
        EventManager.StartListening("holeEntered", OnHoleEntered);
        EventManager.StartListening("ballIsThrown", OnBallThrown);
    }

    public override void OnDisable() {
        EventManager.StopListening("holeEntered", OnHoleEntered);
        EventManager.StopListening("ballIsThrown", OnBallThrown);
    }

    private void OnHoleEntered(Dictionary<string, object> message) {
        int points = (int)message["points"];
        UpdateGainedPoints(points);
    }

    private void OnBallThrown(Dictionary<string, object> message) {
        IncreaseThrownBallsNr();
    }

    public void ChangeXrOriginPos() {
        System.Numerics.Vector3 actualPos = ActualPlayerPos();
        transform.position = new UnityEngine.Vector3(actualPos.X, actualPos.Y, actualPos.Z);
    }

    public bool PlayerCanBeSwitched() {
        if (PlayerIsDone()) {
            SwitchToNextPlayer();        
            return true;
        }
        return false;
    }

    private bool PlayerIsDone() {
        return ((players[actualPlayerNr].GainedPoints >= MAX_NUM_OF_POINTS) ||
            (players[actualPlayerNr].NumberOfThrownBalls >= MAX_NUM_OF_THROWN_BALLS));
    } 

    private void SwitchToNextPlayer() {
        if (actualPlayerNr<numOfPlayers) {
            actualPlayerNr++;
            players[actualPlayerNr].CalculateTotalScore();
        }
    }

    public void UpdateGainedPoints(int points) {
        players[actualPlayerNr].GainedPoints += points;
    }

    public int GainedPoints() {
        return players[actualPlayerNr].GainedPoints;
    }

    public void IncreaseThrownBallsNr() {
        players[actualPlayerNr].NumberOfThrownBalls++;
    }

    public Vector3 ActualPlayerPos() {
        return players[actualPlayerNr].position;
    }

    public bool FinishedGame() {
        if (actualPlayerNr > numOfPlayers)
            return true;
        return false;
    }


    public List<int> GetWinner() {
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

    private void OnDestroy() {
        Finish();
    }

    public void Finish() {
        OnDisable();
        RemoveGameObjects();
    }

    private void RemoveGameObjects() {
        for (int playerNr = 0; playerNr < numOfPlayers; playerNr++) {
            players.RemoveAt(playerNr);
            horses.RemoveAt(playerNr);
        }
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