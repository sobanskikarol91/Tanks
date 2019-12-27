using UnityEngine;

public class PCMovement : Ability
{
    [SerializeField] float speed = 1f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Execute()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rb.position += new Vector2(horizontal, vertical).normalized * speed * Time.deltaTime;
        
    }
}