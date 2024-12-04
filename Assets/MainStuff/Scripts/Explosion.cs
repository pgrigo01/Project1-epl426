//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public class Explosion : MonoBehaviour
//{
//    public GameObject bomb;
//    public float power = 10.0f;
//    public float radius = 5.0f;
//    public float upForce = 1.0f; //explosion dramatic effect to lift it off the ground 
//    public GameObject smallExplosionPrefab;



//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    void FixedUpdate()
//    {
//        if (bomb == enabled)
//        {
//            Invoke("Detonate", 4);

//        }


//    }

//    void Detonate()
//    {
//        Instantiate(smallExplosionPrefab, transform.position, transform.rotation);
//        Vector3 explosionPosition = bomb.transform.position;
//        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
//        foreach (Collider hit in colliders)
//        {

//            Rigidbody rb = hit.GetComponent<Rigidbody>(); //get the component of the hit collider
//            if (rb != null)
//            {
//                rb.AddExplosionForce(power, explosionPosition, radius, upForce, ForceMode.Impulse);

//            }

//        }
//        Destroy(bomb);


//    }

//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject bomb;
    public float power = 10.0f;
    public float radius = 5.0f;
    public float upForce = 1.0f; // Explosion dramatic effect to lift it off the ground
    public GameObject smallExplosionPrefab;

    private bool hasExploded = false; // Flag to ensure the explosion happens only once

    void FixedUpdate()
    {
        if (bomb != null && bomb.activeSelf && !hasExploded)
        {
            hasExploded = true; // Mark as exploded
            Invoke(nameof(Detonate), 4); // Schedule explosion
        }
    }

    void Detonate()
    {
        // Guard to ensure Detonate cannot be invoked multiple times
        if (!hasExploded) return;

        // Instantiate explosion visual effect
        if (smallExplosionPrefab != null)
        {
            Instantiate(smallExplosionPrefab, transform.position, transform.rotation);
        }

        // Perform explosion logic
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
