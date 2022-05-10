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
    private TextMeshProUGUI scoreLabel, playerLabel;
    private string menuScene = "gameMenuScene";
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
        EventManager.StartListening("holeEntered", OnHoleEntered);
        EventManager.StartListening("ballIsThrown", OnBallThrown);
        EventManager.StartListening("playerChanged", OnPlayerChanged);
    }

    public override void OnDisable() {
        EventManager.StopListening("holeEntered", OnHoleEntered);
        EventManager.StopListening("ballIsThrown", OnBallThrown);
        EventManager.StartListening("playerChanged", OnPlayerChanged);
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
        UpdatePlayerNrOnBoard();
    }

    
    private void UpdateScoreOnBoard() {
        scoreLabel.text = "Points: " + gainedPoints.ToString();
    }

    private void UpdateScoreOnBoard() {
        scoreLabel.text = "Thrown Balls: " + numOfThrownBalls.ToString();
    }

    private void UpdatePlayerNrOnBoard(int playerNr) {
        playerLabel.text = "Player Nr: " + actualPlayerNr.ToString();
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
        BallsNumLabel = GameObject.FindGameObjectWithTag("BallsNumLabel").GetComponent<TextMeshProUGUI>();
    }

    private void RestartScene() {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
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

    private void SwitchToMenu() {
        return;
        // SceneManager.LoadScene(menuScene);
    }

    private void EndTheGame() {
        Application.Quit();
    }

    private void OnDestroy() {
        OnDisable();
    }
}