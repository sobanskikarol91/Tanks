using UnityEngine;
using Photon.Pun;


public class Health : MonoBehaviourPun, IPunObservable
{
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;

    public delegate void HealthChangeEventHandler(float current, float max);
    public event HealthChangeEventHandler HealthChange;


    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void DoDamage(float damage)
    {
        currentHealth -= damage;
        photonView.RPC("OnHealthChange", RpcTarget.AllViaServer, currentHealth, maxHealth, damage);
        Debug.Log("Hit");
        Debug.Log($"CurrentHealth: {currentHealth} Damage: {damage}");

        currentHealth--;

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
        //if (Input.GetKeyDown(KeyCode.Q))
        //    DoDamage(10);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentHealth);
        }
        else
        {
            currentHealth = (float)stream.ReceiveNext();
        }
    }
}