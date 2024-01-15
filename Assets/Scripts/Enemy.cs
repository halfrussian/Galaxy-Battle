using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   [SerializeField] GameObject deathVFX;
   [SerializeField] GameObject hitVFX;
   [SerializeField] Transform parent; 
   [SerializeField] int scorePerHit = 15;
   [SerializeField] int hitPoints = 4;


   ScoreBoard scoreBoard; 

   void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        AddRigidBody();
    }

    void AddRigidBody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
        Debug.Log("Rigidbody added with gravity disabled.");
    }

    void OnParticleCollision(GameObject other)
    {

      // Process Hit
        GameObject vvfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vvfx.transform.parent = parent;
        hitPoints--;
        scoreBoard.IncreaseScore(scorePerHit);


      // Kill Enemy
      if(hitPoints < 1) 
      {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        Destroy(gameObject);
      }
    }

}
