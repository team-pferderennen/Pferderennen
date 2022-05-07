using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HorseRaceBoardManager : MonoBehaviour
{
    private GameObject scorePanel, actionPanel;
    private Canvas actionPanelCanvas, scorePanelCanvas; 
    private Button actionMenuBttn1, 
                  actionMenuBttn2, 
                  restartBttn, 
                  goToMenuBttn, 
                  endTheGameBttn;
    private string menuScene = "gameMenuScene";

    void Start() {
        SetupBttns();
        SetupPanels();
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
}