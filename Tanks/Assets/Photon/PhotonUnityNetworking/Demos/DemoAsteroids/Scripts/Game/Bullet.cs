using Photon.Realtime;
using UnityEngine;

namespace Photon.Pun.Demo.Asteroids
{
    public class Bullet : MonoBehaviourPun
    {
        public Player Owner { get; private set; }

        public void Start()
        {
            if (photonView.IsMine)
                Invoke("DestroyAfterTime", 3f);
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (photonView.IsMine)
                PhotonNetwork.Destroy(gameObject);
        }

        public void InitializeBullet(Player owner, Vector3 originalDirection, float lag)
        {
            Owner = owner;
            transform.forward = originalDirection;
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity = originalDirection * 200.0f;
            rigidbody.position += rigidbody.velocity * lag;
        }

        void DestroyAfterTime()
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}