using UnityEngine;


public class HealthBar : MonoBehaviour
{
    public Transform healthImage;
    public Health health;

    private Vector2 originScale;

    private void Awake()
    {
        if (health) health.HealthChange += UpdateBar;
        originScale = healthImage.localScale;
    }

    public void UpdateBar(float current, float max)
    {
        Debug.Log("Update bar");
        healthImage.localScale = new Vector2(current / max, healthImage.localScale.y);
    }
}