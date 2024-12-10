using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Invoke("Die", 1.3f);
            //Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died!");
        // Add death animation or destroy the player object
        Destroy(gameObject);
    }
}

