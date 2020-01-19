using UnityEngine;
using Photon.Pun;

public class WoodWall : Wall
{
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
            DestroyTile(tile);
        }
    }

    private void DestroyTile(Vector3Int tile)
    {
        tilemap.SetTile(tile, null);
    }
}