using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem crashVFX;
    //the whole ship object 
    [SerializeField] public GameObject shipObject;

    
    void OnTriggerEnter(Collider other) 
    {
        StartCrashSequence();  

    }

    void StartCrashSequence()
    {
        crashVFX.Play();
        GetComponent<PlayerControls>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }

    void ReloadLevel()
    {
        shipObject.SetActive(false);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}

