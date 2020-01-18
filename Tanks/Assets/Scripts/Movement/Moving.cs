using Photon.Pun;
using UnityEngine;

public class Moving : MonoBehaviourPun
{
    [SerializeField] float speed = 1f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] AudioClip engineIdleSnd;
    [SerializeField] AudioClip drivingSnd;

    private new Rigidbody2D rigidbody;
    private PhotonView view;
    private Vector2 networkPosition;
    private AudioSource audioSource;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
        rigidbody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        if (photonView.IsMine)
            audioSource.Play();
    }

    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            Move();
            Rotate();
        }
    }

    private void Move()
    {
        float vertical = Input.GetAxis("Vertical");
        Vector2 direction = (-transform.up * vertical).normalized;
        rigidbody.velocity = direction * speed * Time.deltaTime;

        AdjustSound(vertical);
    }

    private void AdjustSound(float vertical)
    {
        if (audioSource.clip != drivingSnd && vertical != 0)
        {
            audioSource.clip = drivingSnd;
            audioSource.Play();
        }
        else if (audioSource.clip != engineIdleSnd && vertical == 0)
        {
            audioSource.clip = engineIdleSnd;
            audioSource.Play();
        }
    }

    private void Rotate()
    {
        float horizontal = -Input.GetAxis("Horizontal");
        transform.Rotate(new Vector3(0, 0, horizontal * rotationSpeed));
    }
}