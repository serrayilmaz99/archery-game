using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideCursor : MonoBehaviour // Hides the cursor when the game is started
{
    // Start is called before the first frame update
    GameManager GameManager;
    void Start()
    {
        Cursor.visible = true;
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (GameManager.gameStarted == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

}
