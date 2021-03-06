using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour // Responsible for movemenets of the targets
{
    [SerializeField]
    private float amplitude = 1f;
    [SerializeField]
    private float timePeriod = 2f;

    private Vector3 startPosition;

   
    private float chanceOfMovement = 0.5f; // Sets the possibility to the targets to move horizontallly


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.localPosition;

        if(Random.Range(0f,1f) >= chanceOfMovement) 
        {
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update() // When uncommented, targets will move according to the chance of movement
    {
        /*
        if (GameManager.gameStarted)
        {
            if ((GameManager.TargetChoices != "Horizontal") && (GameManager.TargetChoices != "Vertical"))
            {
                float theta = Time.timeSinceLevelLoad / timePeriod;
                float distance = Mathf.Sin(theta) * amplitude;
                Vector3 deltaPosition = new Vector3(0, 0, distance);
                transform.position = startPosition + deltaPosition;
            }
        } */
        
    }
}
