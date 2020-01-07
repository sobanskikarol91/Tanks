using Photon.Pun;
using UnityEngine;


public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;

    private float currentHealth;
    private PhotonView view;
    public delegate void HealthChangeEventHandler(float current, float max);
    public event HealthChangeEventHandler HealthChange;


    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void DoDamage(float damage)
    {
        if (view?.IsMine == false) return;

        currentHealth -= damage;
        HealthChange?.Invoke(currentHealth, maxHealth);

        Debug.Log($"Damaged: {currentHealth}");

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        currentHealth = 0;
        Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            DoDamage(10);
    }
}