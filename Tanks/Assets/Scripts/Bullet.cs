using UnityEngine;


public class Bullet : MonoBehaviour
{
     float speed = 10;

    public void InitializeBullet(Vector3 direction, float lag)
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = direction * speed;
        rigidbody.position += rigidbody.velocity * lag;
    }
}