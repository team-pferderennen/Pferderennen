using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public bool panelIsActive;
    private Canvas canvas = null;
    private HorseRaceBoardManager menuManager = null;

    private void Awake() {
        canvas = GetComponent<Canvas>();
    }

    public void Setup(HorseRaceBoardManager menuManager) {
        this.menuManager = menuManager;
        Hide();    
    }

    // Update is called once per frame
    public void Show() {
        canvas.enabled = true;
        panelIsActive = true;
    }

    public void Hide() {
        canvas.enabled = false;
        panelIsActive = false;
    }


}
