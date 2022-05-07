using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public EventManager eventManager;
    private HorseController horseController;
    private ScoreController scoreController;

    private void Start() {
        horseController = GameObject.FindGameObjectWithTag("HorseController").GetComponent<HorseController>();
        scoreController = GameObject.FindGameObjectWithTag("ScoreController").GetComponent<ScoreController>();
        horseController.OnEventEnable(eventManager);
        scoreController.OnEventEnable(eventManager);
    }

    private void OnDestroy() {
        horseController.OnEventDisable(eventManager);
        scoreController.OnEventDisable(eventManager);
    }
}
