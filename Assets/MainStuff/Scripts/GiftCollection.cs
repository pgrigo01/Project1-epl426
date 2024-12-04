using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftCollection : MonoBehaviour
{
    private int Gift = 0;
    private void OnTriggerEnter(Collider other) { 
        if (other.transform.tag == "GiftTag") {
            Gift++;
            Debug.Log(Gift);
            Destroy(other.gameObject);
        } }
}
