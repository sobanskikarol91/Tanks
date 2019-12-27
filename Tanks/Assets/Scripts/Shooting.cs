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
            view.RPC("Fire", RpcTarget.AllViaServer);
        }
    }

    public void Fire(Vector3 position, Quaternion rotation, PhotonMessageInfo info)
    {
        //float lag = (float)(PhotonNetwork.Time - info.SentServerTime);
        //GameObject bullet;

        ///** Use this if you want to fire one bullet at a time **/
        //bullet = Instantiate(BulletPrefab, rigidbody.position, Quaternion.identity) as GameObject;
        //bullet.GetComponent<Bullet>().InitializeBullet(photonView.Owner, (rotation * Vector3.forward), Mathf.Abs(lag));
    }

    [PunRPC]
    private void Fire(PhotonMessageInfo info)
    {
        GameObject bullet =  Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
        bullet.GetComponent<Bullet>().InitializeBullet(lag);
    }
}