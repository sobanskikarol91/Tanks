using UnityEngine;


public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 10;

    public void InitializeBullet(float lag)
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = -transform.up * speed *  100 * Time.deltaTime;
        rigidbody.position += rigidbody.velocity * lag;
    }
}