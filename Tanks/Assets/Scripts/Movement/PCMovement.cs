using UnityEngine;

public class PCMovement : Ability
{
    [SerializeField] float speed = 1f;

    public void Execute()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.position += new Vector3(horizontal, vertical).normalized * speed * Time.deltaTime;
    }
}