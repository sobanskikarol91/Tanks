using Photon.Pun;
using UnityEngine;

public abstract class PowerUp : MonoBehaviourPun
{
    [SerializeField] AudioClip pickUpSnd;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Tank tank = collision.gameObject.GetComponent<Tank>();

        if (PhotonNetwork.IsMasterClient)
        {
            if (photonView.IsMine)
                AudioSource.PlayClipAtPoint(pickUpSnd, transform.position);

            CollectedByPlayer(collision);
            PhotonNetwork.Destroy(gameObject);
        }
    }

    protected abstract void CollectedByPlayer(Collider2D collision);
}