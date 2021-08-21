using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RightButton : MonoBehaviour // Sets the game in Right Side mode
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
        GameManager.TargetChoices = "Right";
    }
}
