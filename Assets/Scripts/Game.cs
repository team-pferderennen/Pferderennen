using System;
using System.Collections.Generic;
using System.Numerics;

public class Game
{
    private const int MAX_NUM_OF_POINTS = 10;
    private const int MAX_NUM_OF_PLAYERS = 3;
    private const int MAX_NUM_OF_THROWN_BALLS = 5;
    private List<Player> players = new List<Player>();
    private List<Horses> horses = new List<Horses>();
    private int numOfPlayers;
    private int actualPlayerNr;

    public Game(int numOfPlayers) {
        this.numOfPlayers = numOfPlayers;
        this.actualPlayerNr = 1;
        AddGameObjects(numOfPlayers);
    }

    public void AddGameObjects(int numOfPlayers) {
        for (playerNr = 0; playerNr < numOfPlayers; playerNr++) {
            Player newPlayer = new Player(playerNr);
            players.Add(newPlayer);
            Horse newHorse = new Horse(playerNr);
            horses.Add(newHorse);
        }
    }

    public void PlayerCanBeSwitched() {
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
        if (actualPlayerNr<numOfPlayers)
            actualPlayerNr++;
    }

    public void UpdateGainedPoints(int points) {
        players[actualPlayerNr].GainedPoints += points;
    }

    public void IncreaseThrownBallsNr() {
        players[actualPlayerNr].NumberOfThrownBalls++;
    }

    public Vector3 ActualPlayerPos() {
        return players[actualPlayerNr].position;
    }

    public void MoveTheHorse(float interpolationRatio) {
        horses[actualPlayerNr].InterpolateMove(interpolationRatio);
    }

    public void SetNewHorsePos(int points) {
        horses[actualPlayerNr].SetNewPos(points);
    }


    private bool FinishedGame() {
        if ((actualPlayerNr+1)==numOfPlayers)
            return true;
        return false;
    }

    public List<int> GetWinner() {
        List<int> winnerPlayersNrs = new List<int>();
        int maxGainedPoints = players.Max(player => player.GainedPoints);
        /* 
        List<int> gainedPoints = new List<int>;
        for (playerNr = 0; playerNr < numOfPlayers; playerNr++)
            gainedPoints.Add(players[playerNr].GainedPoints)
        int maxGainedPoints = gainedPoints.Max(); */
        for (playerNr = 0; playerNr < numOfPlayers; playerNr++) {
            if(maxGainedPoints == players.[playerNr].GainedPoints)
                winnerPlayersNrs.Add(playerNr);
        }
        return winnerPlayersNrs;
    }

    private void End() {
        RemoveGameObjects();
    }

    private void RemoveGameObjects() {
        for (playerNr = 0; playerNr < numOfPlayers; playerNr++) {
            players[playerNr].Remove();
            horses[playerNr].Remove();
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