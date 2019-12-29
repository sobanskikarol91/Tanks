 using UnityEngine;


public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;

    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void DoDamage(float damage)
    {
        currentHealth -= maxHealth;

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        currentHealth = 0;
        Destroy(gameObject);
    }
}