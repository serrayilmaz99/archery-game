using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCollision : MonoBehaviour
{
    [SerializeField]
    private TargetMovement targetMovement;
    [SerializeField]
    private GameObject impactParticles;

    private AudioSource impactSound;

    // Start is called before the first frame update
    void Start()
    {
        targetMovement = GetComponent<TargetMovement>();
        impactSound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            impactSound.volume = other.relativeVelocity.normalized.magnitude;
            impactSound.Play();

            Instantiate(impactParticles, transform.position, impactParticles.transform.rotation);

            GameManager.score++;

            targetMovement.enabled = false;

            other.transform.parent = transform;

            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            GetComponent<Rigidbody>().isKinematic = false;

            GetComponent<Collider>().enabled = false;

            TargetSpawner.totalTargets--;

            Destroy(gameObject, 1f);
        }
    }
}
