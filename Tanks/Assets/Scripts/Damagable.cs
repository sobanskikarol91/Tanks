using Photon.Pun;
using UnityEngine;


public class Damagable : MonoBehaviourPun
{
    [SerializeField] float damage = 20;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DoDamage(collision);
    }

    private void DoDamage(Collision2D collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();

        if (health)
            health.DoDamage(damage);

        photonView.RPC("Destroy", RpcTarget.All);
    }

    [PunRPC]
    void Destroy()
    {
        Destroy(gameObject);
    }
}