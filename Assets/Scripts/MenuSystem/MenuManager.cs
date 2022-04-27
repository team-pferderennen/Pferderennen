using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private string wchackAMoleScene = "WackAMoleScene";
    private string highStrikerScene = "HighStrikerScene";
    private string throwCansScene = "ThrowCansScene";
    private string horseRaceScene = "HorseRaceScene";

    public Button wchackAMoleBttn, 
                  highStrikerBttn,
                  throwCansBttn, 
                  horseRaceBttn, 
                  endTheGameBttn;

    void Start() {
        wchackAMoleBttn.onClick.AddListener(SwitchToWhackAMole);
        highStrikerBttn.onClick.AddListener(SwitchToHighStriker);
        throwCansBttn.onClick.AddListener(SiwtchToThrowCans);
        horseRaceBttn.onClick.AddListener(SwitchToHorseRace);
        endTheGameBttn.onClick.AddListener(EndTheGame);
    }

    private void SwitchToWhackAMole() {
        SceneManager.LoadScene(wchackAMoleScene);
    }

    private void SwitchToHighStriker() {
        SceneManager.LoadScene(highStrikerScene);
    }

    private void SiwtchToThrowCans() {
        SceneManager.LoadScene(throwCansScene);
    }
    
    private void SwitchToHorseRace() {
        SceneManager.LoadScene(horseRaceScene);
    }

    public void EndTheGame() {
        Application.Quit();
    }    
}