using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private float gameLengthInSeconds = 20f;

    public static Vector4[] targetsdata = null;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text timerText;

    [SerializeField]
    private GameObject gameStateUI;

    public static bool gameStarted = false;

    public static int score;
    private float timer;

    private Text gameStateText;
    private Animator gameStateTextAnim;

    [SerializeField]
    private AudioClip startGameChime;

    [SerializeField]
    private AudioClip endGameChime;

    private AudioSource gameStateSounds;

    public static string TargetChoices;

    // Start is called before the first frame update
    void Start()
    {
        gameStateText = gameStateUI.GetComponent<Text>();
        gameStateText.text = "Select Target Positions";

        gameStateTextAnim = gameStateUI.GetComponent<Animator>();
        gameStateTextAnim.SetBool("ShowText", true);

    }
    private void PreStartGame()
    {

        gameStateText = gameStateUI.GetComponent<Text>();
        gameStateText.text = "Hit Space to Play!";

        gameStateTextAnim = gameStateUI.GetComponent<Animator>();
        gameStateTextAnim.SetBool("ShowText", true);

        gameStateSounds = GetComponent<AudioSource>();

        timer = gameLengthInSeconds;

        UpdateScoreBoard();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Button"))
        {
            GameObject[] buttons = GameObject.FindGameObjectsWithTag("Button");
            for (int i = 0; i < 3; i++)
            {
                buttons[i].SetActive(false);
            }
            PreStartGame();

        }
        if (gameStarted == false && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }

        if (gameStarted)
        {
            timer -= Time.deltaTime;

            UpdateScoreBoard();
        }

        if(gameStarted && timer <= 0)
        {
            EndGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void UpdateScoreBoard()
    {
        scoreText.text = score + " Targets";

        timerText.text = Mathf.RoundToInt(timer) + " Seconds";
    }

    private void StartGame()
    {
        score = 0;
        gameStarted = true;
        gameStateTextAnim.SetBool("ShowText", false);
        gameStateSounds.clip = startGameChime;
        gameStateSounds.Play();
    }

    private void EndGame()
    {
        gameStateText.text = "Game Over!\nPress Space to Restart";
        gameStateTextAnim.SetBool("ShowText", true);
        gameStarted = false;
        timer = gameLengthInSeconds;
        gameStateSounds.clip = endGameChime;
        gameStateSounds.Play();

    }
}
