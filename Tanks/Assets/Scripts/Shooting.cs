using UnityEngine;
using Photon.Pun;


public class Shooting : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject bulletPrefab;

    private PhotonView view;


    private void Awake()
    {
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (view.IsMine && Input.GetMouseButtonDown(0))
        {
            view.RPC("Fire", RpcTarget.AllViaServer, spawnPoint.position, spawnPoint.rotation);
        }
    }

    [PunRPC]
    private void Fire(Vector3 position, Quaternion rotation, PhotonMessageInfo info)
    {
        GameObject bullet = Instantiate(bulletPrefab, position, rotation);
        float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
        bullet.GetComponent<Bullet>().InitializeBullet(-transform.up, lag);
    }

    //Photonview.RPC("_SpawnCollider", PhotonTargets.Others, Rb.position, Rb.rotation);
    //[PunRPC]
    //private void _SpawnCollider(Vector3 position, Quaternion rotation)
    //{
    //    GameObject collider;
    //    collider = Instantiate(Enemy, position + Rb.transform.forward * distance, rotation);
    //}
}