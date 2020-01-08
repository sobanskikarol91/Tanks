using UnityEngine;
using Photon.Pun;


public class Health : MonoBehaviourPun
{
    [SerializeField] float maxHealth;

    private float currentHealth;
    public delegate void HealthChangeEventHandler(float current, float max);
    public event HealthChangeEventHandler HealthChange;


    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void DoDamage(float damage)
    {
       // if (photonView.IsMine == false) return;

        currentHealth -= damage;
        base.photonView.RPC("OnHealthChange", RpcTarget.AllViaServer, currentHealth, maxHealth, damage);
        Debug.Log($"CurrentHealth: {currentHealth} Damage: {damage}");

        if (currentHealth <= 0)
            Die();
    }

    [PunRPC]
    private void OnHealthChange(float currentHealth, float maxHealth, float damage)
    {

        HealthChange?.Invoke(currentHealth, maxHealth);
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