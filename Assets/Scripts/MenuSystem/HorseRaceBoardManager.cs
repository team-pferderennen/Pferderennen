using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HorseRaceBoardManager : MonoBehaviour
{
    public Panel scorePanel = null;
    public Panel actionPanel = null;
    public Button actionMenuBttn1, 
                  actionMenuBttn2,
                  restartBttn, 
                  goToMenuBttn, 
                  endTheGameBttn;
    private string menuScene = "gameMenuScene";

    void Start() {
        SetupPanels();
        restartBttn.onClick.AddListener(RestartScene);
        endTheGameBttn.onClick.AddListener(EndTheGame);
        goToMenuBttn.onClick.AddListener(SwitchToMenu);
        actionMenuBttn1.onClick.AddListener(ManageActionMenu);
        actionMenuBttn2.onClick.AddListener(ManageActionMenu);
    }

    private void SetupPanels() {
        scorePanel.Setup(this);
        actionPanel.Setup(this);
        scorePanel.Show();
    }

    private void RestartScene() {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }

    public void ManageActionMenu() {
        if (scorePanel.panelIsActive) {
            scorePanel.Hide();
            actionPanel.Show();
        }
        else {
            scorePanel.Show();
            actionPanel.Hide();
        }
    }

    public void SwitchToMenu() {
        SceneManager.LoadScene(menuScene);
    }

    public void EndTheGame() {
        Application.Quit();
    }
}