using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Transform healthImage;
    public Health health;

    private Vector2 originScale;

    [SerializeField] Color32 red;
    [SerializeField] Color32 green;

    private void Awake()
    {
        if (health) health.HealthChange += UpdateBar;
        originScale = healthImage.localScale;
    }

    public void UpdateBar(float current, float max)
    {
        Debug.Log("Update bar");
        float percantage = current / max;
        healthImage.localScale = new Vector2(percantage, healthImage.localScale.y);
        healthImage.GetComponent<Image>().color = Color.Lerp(red, green, percantage);
    }
}