using UnityEngine;

public class Ammo : PowerUp
{
    protected override void CollectedByPlayer(Collider2D collision)
    {
        collision.gameObject.GetComponent<Shooting>().IncreaseBullets();
    }
}