using UnityEngine;
using Photon.Pun;


public class Health : MonoBehaviourPun, IPunObservable, IRestart
{
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    [SerializeField] AudioClip deathSnd;
    [SerializeField] AudioClip damageSnd;

    public delegate void HealthChangeEventHandler(float current, float max);
    public event HealthChangeEventHandler HealthChange;

    public delegate void DeathEventHandler();
    public event DeathEventHandler Death;
    private bool IsDeath => currentHealth <= 0;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void DoDamage(float damage)
    {
        if (IsDeath) return;

        Debug.Log($"Damage: {damage}");
        currentHealth -= damage;
        photonView.RPC("OnHealthChange", RpcTarget.AllViaServer, currentHealth, maxHealth, damage);
        AudioSource.PlayClipAtPoint(damageSnd, transform.position);
        currentHealth--;

        if (currentHealth <= 0)
            photonView.RPC("OnDie", RpcTarget.All);
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
        currentHealth = 0;
        Death?.Invoke();

        if (photonView.IsMine)
            PhotonNetwork.Destroy(gameObject);
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

    public void Restart()
    {
        currentHealth = maxHealth;
        photonView.RPC("OnHealthChange", RpcTarget.AllViaServer, currentHealth, maxHealth, 0);
    }
}