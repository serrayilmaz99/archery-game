using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrows : MonoBehaviour // Responsible of the arrows to shoot
{

    [SerializeField]
    private GameObject arrowPrefab;

    GameManager GameManager;

    [SerializeField]
    private Transform arrowTransform;

    private AudioSource bowStretchSound;

   // [SerializeField]
    private float maximumArrowForce = 1300f;

    private float currentArrowForce;

    float maxLerpTime = 1;

    float currentLerpTime;

    private Animator bowAnimator;

    // Start is called before the first frame update
    void Start()
    {
        bowAnimator = GetComponent<Animator>();

        bowStretchSound = GetComponent<AudioSource>();

        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameStarted) 
        {
            if (Input.GetMouseButtonDown(0)) // When left mouse button is pressed
            {
                DrawBow();
            }
            if (Input.GetMouseButton(0)) // While the left mouse button is being pressed
            {
                PowerUpBow();
            }
            if (Input.GetMouseButtonUp(0)) // When left mouse button is released
            {
                ReleaseBow();
            }
        }
         
        else if (!GameManager.gameStarted) // Shooting arrows while the game is not started is not allowed
        {
            bowAnimator.SetBool("drawing", false);
            currentArrowForce = 0;
            bowStretchSound.Stop();
        }
    }

    private void DrawBow()
    {
        bowAnimator.SetBool("drawing", true);

        bowStretchSound.Play();
    }

    private void PowerUpBow()
    {
        if (bowAnimator.GetBool("drawing"))
        {
            currentLerpTime += Time.deltaTime;

            if (currentLerpTime > maxLerpTime)
            {
                currentLerpTime = maxLerpTime;
            }

            float perc = currentLerpTime / maxLerpTime;

            currentArrowForce = Mathf.Lerp(0, maximumArrowForce, perc);
        }

    }

    private void ReleaseBow()
    {
        bowAnimator.SetBool("drawing", false);

        GameObject arrow = Instantiate(arrowPrefab, arrowTransform.position, arrowTransform.rotation);

        Destroy(arrow, 5f);
        arrow.GetComponent<Rigidbody>().AddForce(arrowTransform.forward * currentArrowForce);

        currentArrowForce = 0;
        currentLerpTime = 0;

        bowStretchSound.Stop();

    }
}
