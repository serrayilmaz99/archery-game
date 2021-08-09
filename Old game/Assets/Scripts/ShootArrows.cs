using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrows : MonoBehaviour
{

    [SerializeField]
    private GameObject arrowPrefab;

    [SerializeField]
    private Transform arrowTransform;

    private AudioSource bowStretchSound;

    // [SerializeField]
    private float maximumArrowForce = 1300f;

    private float currentArrowForce;

    float maxLerpTime = 1;

    float currentLerpTime;

    private Animator bowAnimator;
    public float arrowtrigger;
    private bool arrowcalldown = true;

    // Start is called before the first frame update
    void Start()
    {
        bowAnimator = GetComponent<Animator>();

        bowStretchSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {


        if (arrowtrigger > 30 && arrowcalldown)
        {
            StartCoroutine(waitseconds(2));
            ReleaseBow();
        }



    }

    private void ReleaseBow()
    {
        bowAnimator.SetBool("drawing", false);

        GameObject arrow = Instantiate(arrowPrefab, arrowTransform.position, arrowTransform.rotation) as GameObject;

        arrow.GetComponent<Rigidbody>().AddForce(arrowTransform.forward * maximumArrowForce);

        currentArrowForce = 0;
        currentLerpTime = 0;

        bowStretchSound.Stop();

    }
    IEnumerator waitseconds(int x)
    {
        arrowcalldown = false;
        yield return new WaitForSeconds(x);
        arrowcalldown = true;
    }
}
