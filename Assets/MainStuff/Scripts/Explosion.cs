using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Explosion : MonoBehaviour
{
    public GameObject bomb;
    public float power = 10.0f;
    public float radius = 5.0f;
    public float upForce = 1.0f; //explosion dramatic effect to lift it off the ground 




    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (bomb == enabled)
        {
            Invoke("Detonate", 4);
        }
    }

    void Detonate()
    {
        Vector3 explosionPosition = bomb.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>(); //get the component of the hit collider
            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPosition, radius, upForce, ForceMode.Impulse);
            }
        }

    }
}
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Explosion : MonoBehaviour
//{
//    public GameObject bomb;
//    public float power = 10.0f;
//    public float radius = 5.0f;
//    public float upForce = 1.0f; // Explosion dramatic effect to lift it off the ground

//    private bool exploded = false;

//    void FixedUpdate()
//    {
//        // Check if the bomb is active and not yet exploded
//        if (bomb.activeSelf && !exploded)
//        {
//            exploded = true; // Prevent multiple explosions
//            Invoke("Detonate", 5); // Schedule the explosion
//        }
//    }

//    void Detonate()
//    {
//        Vector3 explosionPosition = bomb.transform.position;

//        // Find all colliders within the explosion radius
//        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);

//        foreach (Collider hit in colliders)
//        {
//            Rigidbody rb = hit.GetComponent<Rigidbody>(); // Get the Rigidbody component of the hit object
//            if (rb != null)
//            {
//                rb.AddExplosionForce(power, explosionPosition, radius, upForce, ForceMode.Impulse);
//            }
//        }

//        // Optional: Deactivate or destroy the bomb after explosion
//        bomb.SetActive(false);
//    }
//}
