
//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;

//public class GiftCollection : MonoBehaviour
//{
//    private int giftCount = 0; // Renamed for clarity
//    public TextMeshProUGUI giftText;
//    public GameObject giftParticleEffectPrefab; // Particle effect prefab

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("GiftTag"))
//        {
//            AddGifts(1, other.gameObject);
//        }
//        else if (other.CompareTag("GiftCluster4"))
//        {
//            AddGifts(2, other.gameObject);
//        }
//    }

//    private void AddGifts(int count, GameObject gift)
//    {
//        giftCount += count;
//        UpdateGiftText();
//        Debug.Log($"Gifts collected: {giftCount}");

//        // Spawn particle effect at the gift's position
//        if (giftParticleEffectPrefab != null)
//        {
//            Instantiate(giftParticleEffectPrefab, gift.transform.position, Quaternion.identity);
//        }

//        // Destroy the gift object
//        Destroy(gift);
//    }

//    private void UpdateGiftText()
//    {
//        if (giftText != null)
//        {
//            giftText.text = "Gifts: " + giftCount;
//        }
//    }
//}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GiftCollection : MonoBehaviour
{
    private int giftCount = 0; // Collected gifts
    private int totalGifts = 0; // Total gifts in the scene
    public TextMeshProUGUI giftText;
    public GameObject giftParticleEffectPrefab; // Particle effect prefab
    public GameObject gameEndPanel; // Reference to the game-ending panel/UI

    private void Start()
    {
        // Count total gifts in the scene with the tag "GiftTag"
        GameObject[] giftTagObjects = GameObject.FindGameObjectsWithTag("GiftTag");
        totalGifts = giftTagObjects.Length;

        // Log gift information
        Debug.Log("Objects with 'GiftTag':");
        foreach (GameObject obj in giftTagObjects)
        {
            Debug.Log(obj.name);
        }

        // Update the gift text at the start
        UpdateGiftText();
        Debug.Log($"Total gifts in the scene: {totalGifts}");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GiftTag"))
        {
            AddGifts(1, other.gameObject);
        }
    }

    private void AddGifts(int count, GameObject gift)
    {
        giftCount += count;
        UpdateGiftText();
        Debug.Log($"Gifts collected: {giftCount}");

        // Spawn particle effect at the gift's position
        if (giftParticleEffectPrefab != null)
        {
            Instantiate(giftParticleEffectPrefab, gift.transform.position, Quaternion.identity);
        }

        // Destroy the gift object
        Destroy(gift);

        // Check if all gifts are collected
        if (giftCount >= totalGifts)
        {
            EndGame();
        }
    }

    private void UpdateGiftText()
    {
        if (giftText != null)
        {
            giftText.text = "Gifts: " + giftCount + " / " + totalGifts;
        }
    }

    private void EndGame()
    {
        Debug.Log("All gifts collected! Game over!");

        // Show the game-ending UI (e.g., panel)
        if (gameEndPanel != null)
        {
            gameEndPanel.SetActive(true);
        }

        // Optional: Stop the game or disable player controls
        Time.timeScale = 0; // Pause the game
    }
}
