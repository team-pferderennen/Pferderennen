// Playermanager





/// Collider für Würfe, die daneben gegangen sind.



using System;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Numerics;

public class GameController: GameEventListener {
    private const int MAX_NUM_OF_PLAYERS = 3;
    private const float DESIRED_INTERP_DURATION = 0.5f;
    private int playerNr;
    private float elapsedTime;
    public bool newHorsePosIsSet;
    private Game game;
    private TextMeshProUGUI scoreLabel, playerLabel;

    private GameObject xrOrigin;



    private void Start() {
        scoreLabel = GameObject.FindGameObjectWithTag("ScoreLabel").GetComponent<TextMeshProUGUI>();
        playerLabel = GameObject.FindGameObjectWithTag("PlayerLabel").GetComponent<TextMeshProUGUI>();
        xrOrigin = GameObject.FindGameObjectWithTag("XROrigin");
        /********************************
        * TODO: get numOfPlayers from the menu system
        ****************************/
        game = new Game(1);
        OnEnable();
        elapsedTime = 0;
    }

    private void Update() {
        UpdateScoreOnBoard(game.GainedPoints());
        if (game.PlayerCanBeSwitched()) {
            ChangeXrOriginPos();
            UpdatePlayerNrOnBoard(game.ActualPlayerNr);
        }
        if (game.FinishedGame()) {
            List<int> winnerPlayersNrs = game.GetWinner(); 
            // TODO: show the winners on the board for a while
            Debug.Log(winnerPlayersNrs);
            EndTheGame();
        }
    }
    
    void FixedUpdate() {
        if (newHorsePosIsSet) {
            float interpolationRatio = elapsedTime / DESIRED_INTERP_DURATION;
            game.MoveTheHorse(interpolationRatio);
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= DESIRED_INTERP_DURATION)
                newHorsePosIsSet = false;
        }
    }

    private void UpdateScoreOnBoard(int gainedPoints) {
        scoreLabel.text = "Points: " + gainedPoints.ToString();
    }

    private void UpdatePlayerNrOnBoard(int playerNr) {
        playerLabel.text = "Player Nr: " + playerNr.ToString();
    }

    public void ChangeXrOriginPos() {
        System.Numerics.Vector3 actualPos = game.ActualPlayerPos();
        xrOrigin.transform.position = new UnityEngine.Vector3(actualPos.X, actualPos.Y, actualPos.Z);
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
        game.UpdateGainedPoints(points);
        game.SetNewHorsePos(points);
        newHorsePosIsSet = true;
        elapsedTime = 0;
    }

    private void OnBallThrown(Dictionary<string, object> message) {
        game.IncreaseThrownBallsNr();
    }

    private void EndTheGame() {
        OnDisable();
        game.End();
        // TODO: Switch to menu scene
    }

    private void OnDestroy() {
        OnDisable();
        game.End();
    }
}
