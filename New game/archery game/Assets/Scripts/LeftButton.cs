using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftButton : MonoBehaviour // Sets the game in Left Side mode
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
        GameManager.TargetChoices = "Left";
    }
}
