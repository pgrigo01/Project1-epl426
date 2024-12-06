//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Explosion_Damage : MonoBehaviour
//{
//    public GameObject bomb;
//    public float power = 10.0f;
//    public float radius = 0.01f;
//    public float upForce = 1.0f; // Explosion dramatic effect to lift it off the ground
//    public GameObject smallExplosionPrefab;

//    private bool hasExploded = false; // Flag to ensure the explosion happens only once
//    public void OnCollisionEnter(Collision collision)
//    {
//        if(collision.rigidbody != null)
//        {
//            Debug.Log(collision.rigidbody.name);
//            //Detonate();
//            Invoke("Detonate", 4);
//            Animator colorchange = GetComponent<Animator>();
//            colorchange.enabled =true;

//        }

//    }   
//    //void FixedUpdate()
//    //{
//    //    if (bomb != null && bomb.activeSelf && !hasExploded)
//    //    {
//    //        hasExploded = true; // Mark as exploded
//    //        Invoke(nameof(Detonate), 4); // Schedule explosion
//    //    }
//    //}

//    void Detonate()
//    {
//        // Guard to ensure Detonate cannot be invoked multiple times
//        //if (!hasExploded) return;

//        //// Instantiate explosion visual effect
//        //if (smallExplosionPrefab != null)
//        //{
//        //    Instantiate(smallExplosionPrefab, transform.position, transform.rotation);
//        //}

//        Instantiate(smallExplosionPrefab, transform.position, transform.rotation);
//        Vector3 explosionPosition = bomb.transform.position;
//        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
//        foreach (Collider hit in colliders)
//        {
//            Rigidbody rb = hit.GetComponent<Rigidbody>();
//            if (rb != null)
//            {   
//                rb.AddExplosionForce(power, explosionPosition, radius, upForce, ForceMode.Impulse);
//            }
//        }

//        //// Destroy the bomb to prevent re-triggering
//        //Destroy(bomb);

//        // Optionally destroy this GameObject
//        Destroy(gameObject);
//    }
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Damage : MonoBehaviour
{
    public GameObject bomb;
    public float power = 10.0f;
    public float radius = 5.0f; // Adjust to set the proximity for explosion effect
    public float upForce = 1.0f; // Explosion dramatic effect to lift it off the ground
    public GameObject smallExplosionPrefab;
    public float triggerDistance = 0.3f; // Distance at which the player triggers the bomb

    private bool hasExploded = false; // Flag to ensure the explosion happens only once
    private Animator colorChangeAnimator; // Reference to the Animator component
    private Transform playerTransform; // Reference to the player's transform

    void Start()
    {
        // Cache the Animator component
        colorChangeAnimator = GetComponent<Animator>();
        if (colorChangeAnimator != null)
        {
            colorChangeAnimator.enabled = false; // Ensure it's disabled initially
        }

        // Find the player GameObject (assuming the tag is "Player")
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found! Ensure the Player GameObject is tagged 'Player'.");
        }
    }

    void Update()
    {
        // Check distance to the player
        if (playerTransform != null && Vector3.Distance(transform.position, playerTransform.position) <= triggerDistance)
        {
            TriggerBomb();
        }
    }

    private void TriggerBomb()
    {
        if (!hasExploded)
        {
            // Enable the Animator
            if (colorChangeAnimator != null && !colorChangeAnimator.enabled)
            {
                colorChangeAnimator.enabled = true;
            }

            // Schedule detonation
            hasExploded = true; // Ensure detonation happens only once
            Invoke("Detonate", 4);
        }
    }

    void Detonate()
    {
        // Instantiate explosion visual effect
        if (smallExplosionPrefab != null)
        {
            Instantiate(smallExplosionPrefab, transform.position, transform.rotation);
        }

        Vector3 explosionPosition = bomb.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPosition, radius, upForce, ForceMode.Impulse);
            }
        }

        // Destroy the bomb to prevent re-triggering
        Destroy(bomb);

        // Optionally destroy this GameObject
        Destroy(gameObject);
    }
}
