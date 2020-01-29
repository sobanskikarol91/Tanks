using UnityEngine;

public class Ammo : PowerUp
{
    protected override void CollectedByPlayer(Collider2D collision)
    {
        Shooting shooting = collision.gameObject.GetComponent<Shooting>();

        if (shooting)
            shooting.IncreaseBullets();
    }
}