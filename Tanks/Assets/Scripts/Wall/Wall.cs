using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Wall : MonoBehaviour
{
    [SerializeField] protected AudioClip hitSound;

    protected Tilemap tilemap;

    protected virtual void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(hitSound, transform.position);
        OnCollisionEffect(collision);
    }

    protected abstract void OnCollisionEffect(Collision2D collision);
}