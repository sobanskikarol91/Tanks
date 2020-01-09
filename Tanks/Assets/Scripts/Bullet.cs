using UnityEngine;
using Photon.Realtime;

public class Bullet : MonoBehaviour
{
    public Player Owner { get; private set; }
    [SerializeField] float speed = 10;

    public void InitializeBullet(Player owner, Vector3 direction, float lag)
    {
        Owner = owner;
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = direction * speed;
        rigidbody.position += rigidbody.velocity * lag;
    }
}