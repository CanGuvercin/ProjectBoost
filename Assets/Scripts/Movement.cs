using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float ForceMultiplier= 100f;
    [SerializeField] float RouteForce = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    [SerializeField] ParticleSystem leftThrustersParticles;

    Rigidbody rb;
    //AudioSource audioSourceMaster;
    AudioSource audioSource;

    bool isAlive;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //audioSourceMaster = GetComponent<AudioSource>();
        //audioSourceMaster.playOnAwake = false;
        audioSource = GetComponent<AudioSource>();
        
    }

    void Update()
    {
        ProcessInput();
        ProcessRotation();
    }

    void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * ForceMultiplier * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            if (mainEngineParticles.isPlaying)
            {
                mainEngineParticles.Play();
            }
        }
        else
        {
            {
                mainEngineParticles.Stop();
                audioSource.Stop();
            }
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward*RouteForce*Time.deltaTime);
            if (!leftThrustersParticles.isPlaying)
            {
                leftThrustersParticles.Play();
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(-Vector3.forward*RouteForce*Time.deltaTime);
            if (!rightThrusterParticles.isPlaying)
            {
                rightThrusterParticles.Play();
            }
        }
        else
        {
            rightThrusterParticles.Stop();
            leftThrustersParticles.Stop();
        }
    }
}
