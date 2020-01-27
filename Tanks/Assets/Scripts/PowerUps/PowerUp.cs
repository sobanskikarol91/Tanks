using Photon.Pun;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Tank tank = collision.gameObject.GetComponent<Tank>();

        if (tank && tank.photonView.IsMine)
        {
            CollectedByPlayer(collision);
            PhotonNetwork.Destroy(gameObject);
        }
    }

    protected abstract void CollectedByPlayer(Collider2D collision);
}