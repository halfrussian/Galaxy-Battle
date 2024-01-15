using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.WSA;

public class PlayerControls : MonoBehaviour
{

    [Header("General Setup Setting")]

    [Tooltip("How fast ship moves up and down")]
    [SerializeField] float controlSpeed = 10; 

    [Tooltip("How fast player moves horisontallty")]
    [SerializeField] float xRange = 10f;

    [Tooltip("How fast player moves vertically")]
    [SerializeField] float yRange = 3.5f;

    [Header("Laser Gun Array ")]
    [SerializeField] GameObject[] lasers;


    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 3f;

    [Header("Playewr input based tuning")]
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float controlRollFactor = -20f;



    float xThrow, yThrow;

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessRotation() 
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);

    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");


        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
    void ProcessFiring() {

        if (Input.GetButton("Fire1")) {
            SetLasersActive(true);
        } else {
           SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive)
    {
     //for each of the laser, we have active them   
     foreach (GameObject laser in lasers) 
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }

 
}
