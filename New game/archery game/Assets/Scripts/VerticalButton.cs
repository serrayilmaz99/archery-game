using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerticalButton : MonoBehaviour // Sets the game in Vertical mode
{
    public Button Button;
    GameManager GameManager;

    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Button btn = Button.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        GameManager.TargetChoices = "Vertical";
    }
}

