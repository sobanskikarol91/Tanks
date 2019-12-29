using Photon.Pun;
using UnityEngine;

public class PCMovement : MonoBehaviour
{
    [SerializeField] float speed = 1f;

    private Rigidbody2D rigidbody;
    private PhotonView view;
    private Vector2 networkPosition;


    private void Awake()
    {
        view = GetComponent<PhotonView>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (view.IsMine)
            Move();
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        rigidbody.velocity = new Vector2(horizontal, vertical).normalized * speed * Time.fixedDeltaTime;
    }
}