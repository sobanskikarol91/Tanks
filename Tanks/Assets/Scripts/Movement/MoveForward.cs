using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] float speed;

    private void Awake()
    {
        Destroy(gameObject, 5);
    }

    private void Update()
    {
        transform.position +=  -transform.up * speed * Time.deltaTime;
    }
}