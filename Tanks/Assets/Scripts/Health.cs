using UnityEngine;
using Photon.Pun;


public class Health : MonoBehaviourPun, IPunObservable
{
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    [SerializeField] AudioClip deathSnd;
    [SerializeField] AudioClip damageSnd;

    public delegate void HealthChangeEventHandler(float current, float max);
    public event HealthChangeEventHandler HealthChange;

    public delegate void DeathEventHandler();
    public event DeathEventHandler Death;


    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void DoDamage(float damage)
    {
        currentHealth -= damage;
        photonView.RPC("OnHealthChange", RpcTarget.AllViaServer, currentHealth, maxHealth, damage);
        AudioSource.PlayClipAtPoint(damageSnd, transform.position);
        currentHealth--;

        if (currentHealth <= 0)
            photonView.RPC("OnDie", RpcTarget.AllViaServer);
    }

    [PunRPC]
    private void OnHealthChange(float currentHealth, float maxHealth, float damage)
    {
        HealthChange?.Invoke(currentHealth, maxHealth);
    }

    [PunRPC]
    private void OnDie()
    {
        AudioSource.PlayClipAtPoint(deathSnd, transform.position);
        Death?.Invoke();
        currentHealth = 0;
        Destroy(gameObject);
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