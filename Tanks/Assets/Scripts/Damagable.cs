using Photon.Pun;
using UnityEngine;


public class Damagable : MonoBehaviourPun
{
    [SerializeField] float damage = 20;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (photonView)
            photonView.RPC("DoDamage", RpcTarget.AllViaServer, collision);
        else
            Debug.Log("there is no view component attached");
    }

    [PunRPC]
    private void DoDamage(Collision2D collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();

        if (health)
            health.DoDamage(damage);
    }
}