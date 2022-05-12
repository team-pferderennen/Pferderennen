// Playermanager

/// Collider für Würfe, die daneben gegangen sind.

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using UnityEngine;

 public enum gameState {
   none,
   ballIsThrown,
   ballIsBack,
   playerisReady,
   playerIsFinished,
   waitingForPlayerToSwitch,
   playerCanBeSwitched,
   waitingForFinish,
   gameCanBeFinished, 
   finishedGame
 }


public class PlayersManager: GameEventListener
{
    public const int MAX_NUM_OF_POINTS = 10;
    private const int NUM_OF_PLAYERS = 3;
    private const int MAX_NUM_OF_THROWN_BALLS = 5;
    private int numOfPlayers;
    private int actualPlayerNr;
    private gameState state = gameState.none;

    private List<Player> players = new List<Player>();


    private void Start() {
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
        switch(state) {
            case gameState.finishedGame:
                break;
            case gameState.gameCanBeFinished:
                FinishTheGame();
                break;
            case gameState.waitingForFinish:
                break;
            case gameState.playerCanBeSwitched:
                CheckIfPlayerIsCreated();
                ChangeThePlayer();
                break;
            case gameState.waitingForPlayerToSwitch:
                break;
            case gameState.playerIsFinished:
                CalculateTheScoreOfActualPlayer();
                CheckForNextPlayer();
                break;
            case gameState.playerisReady:
                CheckIfPlayerIsFinished();
                break;
            default:
                CheckIfPlayerIsCreated();
                break;
        }
    }

    public void AddPlayers(int numOfPlayers, System.Numerics.Vector3 startPos) {
        for (int playerNr = 1; playerNr <= numOfPlayers; playerNr++) {
            Player newPlayer = new Player(playerNr, startPos);
            players.Add(newPlayer);
        }
        state = gameState.playerisReady;
    }


    public override void OnEnable() {
        EventManager.StartListening("holeEntered", this.OnHoleEntered);
        EventManager.StartListening("ballIsThrown", this.OnBallThrown);
        EventManager.StartListening("ballIsBack", this.OnBallIsBack);
    }

    public override void OnDisable() {
        EventManager.StopListening("holeEntered", this.OnHoleEntered);
        EventManager.StopListening("ballIsThrown", this.OnBallThrown);
        EventManager.StopListening("ballIsBack", this.OnBallIsBack);
    }

    private void OnHoleEntered(Dictionary<string, object> message) {
        int points = (int)message["points"];
        UpdateGainedPoints(points);
    }

    private void OnBallThrown(Dictionary<string, object> message) {
        IncreaseThrownBallsNr();
    }

    private void OnBallIsBack(Dictionary<string, object> message) {
        if (state == gameState.waitingForPlayerToSwitch)
            state = gameState.playerCanBeSwitched;
        else if(state == gameState.waitingForFinish)
            state = gameState.gameCanBeFinished;
    }

    private void FinishTheGame() {
        /********************************
        * TODO: show the winners on the board for a while (EventManager)
        * TODO: switch to game menu
        ****************************/
        // List<int> winnerPlayersNrs = GetWinner(); 
        // Debug.Log("Winners" + winnerPlayersNrs[0]);
        Dictionary<string, object> playerTotalScores = new Dictionary<string, object>();
        string totalScoreKey;
        int totalScore;
        for (int playerNr = 1; playerNr <= NUM_OF_PLAYERS; playerNr++) {
            totalScoreKey = "totalScore" + playerNr.ToString();
            totalScore = players[playerNr-1].TotalScore;
            playerTotalScores.Add(totalScoreKey, totalScore);
        }
        EventManager.TriggerEvent("gameFinished", playerTotalScores);
        state = gameState.finishedGame;
        OnDisable();
    }

    private void ChangeThePlayer() {
        ChangeXrOriginPos();
        EventManager.TriggerEvent("playerChanged", null);
    }

    private void ChangeXrOriginPos() {
        System.Numerics.Vector3 actualPos = ActualPlayerPos();
        transform.position = new UnityEngine.Vector3(actualPos.X, actualPos.Y, actualPos.Z);
    }

    private void CheckIfPlayerIsCreated() {
        if (players[actualPlayerNr-1] != null)
            state = gameState.playerisReady;
    }

    private void CheckIfPlayerIsFinished() {
        if ((players[actualPlayerNr-1].GainedPoints >= MAX_NUM_OF_POINTS) ||
                (players[actualPlayerNr-1].NumberOfThrownBalls >= MAX_NUM_OF_THROWN_BALLS))
            state = gameState.playerIsFinished;
    } 

    private void CheckForNextPlayer() {
        if(actualPlayerNr < NUM_OF_PLAYERS) {
            actualPlayerNr++;
            state = gameState.waitingForPlayerToSwitch;
        }
        else
            state = gameState.waitingForFinish;
    }

    private void CalculateTheScoreOfActualPlayer() {
            players[actualPlayerNr-1].CalculateTotalScore();
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

    private List<int> GetWinner() {
        List<int> winnerPlayersNrs = new List<int>();
        int maxGainedPoints = players.Max(player => player.TotalScore);
        /* 
        List<int> gainedPoints = new List<int>;
        for (playerNr = 0; playerNr < numOfPlayers; playerNr++)
            gainedPoints.Add(players[playerNr].GainedPoints)
        int maxGainedPoints = gainedPoints.Max(); */
        for (int playerNr = 0; playerNr < NUM_OF_PLAYERS; playerNr++) {
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