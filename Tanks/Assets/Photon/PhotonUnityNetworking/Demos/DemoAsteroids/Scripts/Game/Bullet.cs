using Photon.Realtime;
using UnityEngine;

namespace Photon.Pun.Demo.Asteroids
{
    public class Bullet : MonoBehaviourPun
    {
        public Player Owner { get; private set; }
        public float speed = 200f;

        private new Rigidbody rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (photonView.IsMine)
                PhotonNetwork.Destroy(gameObject);
        }

        public void InitializeBullet(Player owner, Vector3 originalDirection, float lag)
        {
            Owner = owner;
            transform.forward = originalDirection.normalized;

            rigidbody.velocity = originalDirection * speed;
            rigidbody.position += rigidbody.velocity * lag;
        }

        private void FixedUpdate()
        {
            rigidbody.velocity = rigidbody.velocity.normalized * speed;
        }
    }
}