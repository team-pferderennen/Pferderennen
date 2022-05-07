using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : GameEventListener
{
    private HorseController horseController;

    private void Awake() {
        horseController = GameObject.FindGameObjectWithTag("HorseController").GetComponent<HorseController>();
    }

    public static int MAX_NUM_OF_POINTS = 10;
    public bool newHoleEnter;
    public int actualPoints;
    private int gainedPoints;
    public EventManager Event;

    public override void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void Start() {
        newHoleEnter = false;
        gainedPoints = 0;
        OnEnable();
    }

    private void Update()
    {
        if (gainedPoints >= MAX_NUM_OF_POINTS)
            Debug.Log("Next Player!");
    }

    public override void OnEventRaised(int points) {
        gainedPoints += points;
        //horseController.OnNewPosition(points);
    }
    
}
