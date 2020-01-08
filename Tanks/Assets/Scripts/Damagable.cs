using Photon.Pun;
using UnityEngine;


public class Damagable : MonoBehaviourPun
{
    [SerializeField] float damage = 20;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (photonView)
            DoDamage(collision);
        else
            Debug.Log("there is no view component attached");
    }

    private void DoDamage(Collision2D collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();

        if (health)
            health.DoDamage(damage);

        PhotonNetwork.Destroy(photonView);
    }
}