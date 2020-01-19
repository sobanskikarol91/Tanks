using UnityEngine;
using Photon.Pun;

public class WoodWall : Wall
{
    private PhotonView photonView;

    protected override void Awake()
    {
        base.Awake();
        photonView = GetComponent<PhotonView>();
    }

    protected override void OnCollisionEffect(Collision2D collision)
    {
        FindHitTile(collision);
    }

    void FindHitTile(Collision2D collision)
    {
        Vector3 hitPosition = Vector3.zero;

        foreach (ContactPoint2D hit in collision.contacts)
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 position = hit.point - rb.velocity * 0.01f;
            Vector3Int tile = tilemap.WorldToCell(position);

            photonView.RPC("DestroyTile", RpcTarget.All, (Vector3)tile);
        }
    }

    [PunRPC]
    private void DestroyTile(Vector3 tile)
    {
        Vector3Int position = new Vector3Int((int)tile.x, (int)tile.y, 0);
        tilemap.SetTile(position, null);
    }
}