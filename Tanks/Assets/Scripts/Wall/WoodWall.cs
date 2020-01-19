using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using System.Linq;

public class WoodWall : Wall, IRestart
{
    private PhotonView photonView;

    private Dictionary<Vector3Int, TileBase> changedTiles = new Dictionary<Vector3Int, TileBase>();

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
        TileBase baseTile = tilemap.GetTile(position);
        if (baseTile == null) return;

        changedTiles.Add(position, baseTile);
        tilemap.SetTile(position, null);
    }

    public void Restart()
    {
        var values = changedTiles.Keys.ToArray();

        for (int i = 0; i < changedTiles.Count; i++)
        {
            Vector3Int position = values[i];
            TileBase tile = changedTiles[position];
            tilemap.SetTile(position, tile);
        }

        changedTiles.Clear();
    }
}