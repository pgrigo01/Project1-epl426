
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Explosion_Damage : MonoBehaviour
//{
//    public GameObject bomb;
//    public float damage = 100.0f;
//    public float power = 10.0f;
//    public float radius = 5.0f; // Adjust to set the proximity for explosion effect
//    public float upForce = 1.0f; // Explosion dramatic effect to lift it off the ground
//    public GameObject smallExplosionPrefab;
//    public float triggerDistance = 0.3f; // Distance at which the player triggers the bomb

//    private bool hasExploded = false; // Flag to ensure the explosion happens only once
//    private Animator colorChangeAnimator; // Reference to the Animator component
//    private Transform playerTransform; // Reference to the player's transform

//    void Start()
//    {
//        // Cache the Animator component
//        colorChangeAnimator = GetComponent<Animator>();
//        if (colorChangeAnimator != null)
//        {
//            colorChangeAnimator.enabled = false; // Ensure it's disabled initially
//        }

//        // Find the player GameObject (assuming the tag is "Player")
//        GameObject player = GameObject.FindGameObjectWithTag("Player");
//        if (player != null)
//        {
//            playerTransform = player.transform;
//        }
//        else
//        {
//            Debug.LogError("Player not found! Ensure the Player GameObject is tagged 'Player'.");
//        }
//    }

//    void Update()
//    {
//        // Check distance to the player
//        if (playerTransform != null && Vector3.Distance(transform.position, playerTransform.position) <= triggerDistance)
//        {
//            TriggerBomb();
//        }
//    }

//    private void TriggerBomb()
//    {
//        if (!hasExploded)
//        {
//            // Enable the Animator
//            if (colorChangeAnimator != null && !colorChangeAnimator.enabled)
//            {
//                colorChangeAnimator.enabled = true;
//            }

//            // Schedule detonation
//            hasExploded = true; // Ensure detonation happens only once
//            Invoke("Detonate", 4);
//        }
//    }

//    void Detonate()
//    {
//        // Instantiate explosion visual effect
//        if (smallExplosionPrefab != null)
//        {
//            Instantiate(smallExplosionPrefab, transform.position, transform.rotation);
//        }

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

//        // Destroy the bomb to prevent re-triggering
//        Destroy(bomb);

//        // Optionally destroy this GameObject
//        Destroy(gameObject);
//    }
//    void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player")) // Check if it's the player
//        {
//            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
//            if (playerHealth != null)
//            {
//                playerHealth.TakeDamage(damage);
//            }
//        }
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Damage : MonoBehaviour
{
    public GameObject bomb;
    public float damage = 100.0f;
    public float power = 10.0f;
    public float radius = 5.0f; // Explosion effect radius
    public float upForce = 1.0f; // Explosion lift effect
    public GameObject smallExplosionPrefab;
    public float triggerDistance = 0.3f; // Distance at which the player triggers the bomb

    private bool hasExploded = false; // Ensure explosion happens only once
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
            Invoke("Detonate", 4); // Detonate after 4 seconds
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

        // Apply force and check for player damage
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
        foreach (Collider hit in colliders)
        {
            // Apply explosion force to nearby rigidbodies
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPosition, radius, upForce, ForceMode.Impulse);
            }

            // Check if the player is within the explosion radius
            if (hit.CompareTag("Player"))
            {
                PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage); // Damage the player
                }
            }
        }

        // Destroy the bomb to prevent re-triggering
        Destroy(bomb);

        // Optionally destroy this GameObject
        Destroy(gameObject);
    }
}
