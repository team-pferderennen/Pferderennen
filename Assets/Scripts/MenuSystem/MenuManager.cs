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
    private Light lightComp;

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
        lightComp = GameObject.FindGameObjectWithTag("Light").GetComponent<Light>();
        lightComp.enabled = true;
    }

    private void SwitchToWhackAMole() {
        SceneManager.LoadScene(wchackAMoleScene);
        lightComp.enabled = false;
    }

    private void SwitchToHighStriker() {
        SceneManager.LoadScene(highStrikerScene);
        lightComp.enabled = false;
    }

    private void SiwtchToThrowCans() {
        SceneManager.LoadScene(throwCansScene);
        lightComp.enabled = false;
    }
    
    private void SwitchToHorseRace() {
        SceneManager.LoadScene(horseRaceScene);
        lightComp.enabled = false;
    }

    public void EndTheGame() {
        Application.Quit();
    }    
}