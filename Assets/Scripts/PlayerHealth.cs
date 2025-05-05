using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Vida del jugador: " + currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("El jugador ha muerto");
            Destroy(gameObject); // O implementar lógica de muerte
        }
    }
}
