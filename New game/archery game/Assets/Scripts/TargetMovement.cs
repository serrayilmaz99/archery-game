using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    [SerializeField]
    private float amplitude = 1f;
    [SerializeField]
    private float timePeriod = 2f;

    private Vector3 startPosition;

   
    private float chanceOfMovement = 0.5f;


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
    void Update()
    {
        if (GameManager.gameStarted)
        {
            if (GameManager.level >= 4 && GameManager.TargetChoices == "Left")
            {
                float theta = Time.timeSinceLevelLoad / timePeriod;
                float distance = Mathf.Sin(theta) * amplitude;
                Vector3 deltaPosition = new Vector3(0, 0, distance);
                transform.position = startPosition + deltaPosition;
            }
        }
        
    }
}
