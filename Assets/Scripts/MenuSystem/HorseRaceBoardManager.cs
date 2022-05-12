using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class HorseRaceBoardManager: GameEventListener
{
    private GameObject scorePanel, actionPanel;
    private Canvas actionPanelCanvas, scorePanelCanvas; 
    private Button actionMenuBttn1, actionMenuBttn2, restartBttn, 
                   goToMenuBttn, endTheGameBttn;
    private TextMeshProUGUI scoreLabel, playerLabel, 
                            ballsNumLabel, totalScoreLabel;
    private int actualPlayerNr, numOfThrownBalls, gainedPoints;

    void Start() {
        SetupBttns();
        SetupLabels();
        SetupPanels();
        actualPlayerNr = 1;
        numOfThrownBalls = 0;
        gainedPoints = 0;
        OnEnable();
    }

    public override void OnEnable() {
        EventManager.StartListening("holeEntered", this.OnHoleEntered);
        EventManager.StartListening("ballIsThrown", this.OnBallThrown);
        EventManager.StartListening("playerChanged", this.OnPlayerChanged);
        EventManager.StartListening("gameFinished", this.OnGameFinished);
    }

    public override void OnDisable() {
        EventManager.StopListening("holeEntered", this.OnHoleEntered);
        EventManager.StopListening("ballIsThrown", this.OnBallThrown);
        EventManager.StopListening("playerChanged", this.OnPlayerChanged);
        EventManager.StopListening("gameFinished", this.OnGameFinished);
    }

    private void OnHoleEntered(Dictionary<string, object> message) {
        int newPoints = (int)message["points"];
        gainedPoints += newPoints;
        UpdateScoreOnBoard();
    }

    private void OnBallThrown(Dictionary<string, object> message) {
        numOfThrownBalls++;
        UpdateNumOfThrownBallsOnBoard();
    }


    private void OnPlayerChanged(Dictionary<string, object> message) {
        actualPlayerNr++;
        ResetPlayerInfoBoard();
    }

    private void OnGameFinished(Dictionary<string, object> message) {
        OnDisable();
        ManageResultMode(true);
        UpdateTotalScoreOnBoard(message);
    }

    private void ResetPlayerInfoBoard() {
        numOfThrownBalls = 0;
        gainedPoints = 0;
        UpdatePlayerNrOnBoard();
        UpdateScoreOnBoard();
        UpdateNumOfThrownBallsOnBoard();
    }

    
    private void UpdateScoreOnBoard() {
        scoreLabel.text = "Points: " + gainedPoints.ToString();
    }

    private void UpdateNumOfThrownBallsOnBoard() {
        ballsNumLabel.text = "Thrown Balls: " + numOfThrownBalls.ToString();
    }

    private void UpdatePlayerNrOnBoard() {
        playerLabel.text = "Player Nr: " + actualPlayerNr.ToString();
    }

    private void UpdateTotalScoreOnBoard(Dictionary<string, object> playersTotalScores) {
        int numOfPlayers = playersTotalScores.Count, totalScore;
        string totalScoreKey, playersTotalScoresText = "";
        for (int playerNr=1; playerNr<=numOfPlayers; playerNr++) {
            totalScoreKey = "totalScore" + playerNr.ToString();
            totalScore = (int)playersTotalScores[totalScoreKey];
            playersTotalScoresText += "Player Nr" + playerNr.ToString() + ":\n" + totalScore.ToString() + "\n";
        }
        totalScoreLabel.text = "Result\n" + playersTotalScoresText;
    }

    private void ManageActionMenu() {
        if (scorePanelCanvas.enabled) {
            actionPanelCanvas.enabled = true;
            scorePanelCanvas.enabled = false;
        }
        else {
            actionPanelCanvas.enabled = false;
            scorePanelCanvas.enabled = true;
        }
    }

    private void ManageResultMode(bool enabled) {
        totalScoreLabel.enabled = enabled;
        scoreLabel.enabled = !enabled;    
        playerLabel.enabled = !enabled;    
        ballsNumLabel.enabled = !enabled;    
    }

    private void RestartScene() {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }

    private void SwitchToMenu() {
        return;
        // SceneManager.LoadScene(menuScene);
    }

    private void EndTheGame() {
        Application.Quit();
    }

    private void SetupPanels() {
        scorePanel = GameObject.FindGameObjectWithTag("ScorePanel");
        actionPanel = GameObject.FindGameObjectWithTag("BttnPanel");
        actionPanelCanvas = actionPanel.GetComponent<Canvas>();
        scorePanelCanvas = scorePanel.GetComponent<Canvas>();
        actionPanelCanvas.enabled = false;
        scorePanelCanvas.enabled = true;
    }

    private void SetupBttns() {
        restartBttn = GameObject.FindGameObjectWithTag("RestartBttn").GetComponent<Button>();
        endTheGameBttn = GameObject.FindGameObjectWithTag("EndBttn").GetComponent<Button>();
        goToMenuBttn = GameObject.FindGameObjectWithTag("MenuBttn").GetComponent<Button>();
        actionMenuBttn1 = GameObject.FindGameObjectWithTag("ActionBttn1").GetComponent<Button>();
        actionMenuBttn2 = GameObject.FindGameObjectWithTag("ActionBttn2").GetComponent<Button>();
        restartBttn.onClick.AddListener(RestartScene);
        endTheGameBttn.onClick.AddListener(EndTheGame);
        goToMenuBttn.onClick.AddListener(SwitchToMenu);
        actionMenuBttn1.onClick.AddListener(ManageActionMenu);
        actionMenuBttn2.onClick.AddListener(ManageActionMenu);
    }

    private void SetupLabels() {
        scoreLabel = GameObject.FindGameObjectWithTag("ScoreLabel").GetComponent<TextMeshProUGUI>();
        playerLabel = GameObject.FindGameObjectWithTag("PlayerLabel").GetComponent<TextMeshProUGUI>();
        ballsNumLabel = GameObject.FindGameObjectWithTag("BallsNumLabel").GetComponent<TextMeshProUGUI>();
        totalScoreLabel = GameObject.FindGameObjectWithTag("TotalScoreLabel").GetComponent<TextMeshProUGUI>();
        ManageResultMode(enabled=false);
    }
}