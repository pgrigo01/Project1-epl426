using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GiftCollection : MonoBehaviour
{
    private int Gift = 0;

    public TextMeshProUGUI giftText;
    //public TextMeshProUGUI giftText;
    private void OnTriggerEnter(Collider other) { 
        if (other.transform.tag == "GiftTag") {
            Gift++;
            giftText.text = "Gifts: " + Gift.ToString();
            //giftText.text = "Gift: " + Gift.ToString();
            Debug.Log(Gift);
            Destroy(other.gameObject);
        }
        else if (other.transform.tag == "GiftCluster4")
        {
            Gift+=4;
            giftText.text = "Gifts: " + Gift.ToString();
            //giftText.text = "Gift: " + Gift.ToString();
            Debug.Log(Gift);
            Destroy(other.gameObject);
        }


    }
}
