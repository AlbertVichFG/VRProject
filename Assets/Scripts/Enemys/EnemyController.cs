using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]
    private float maxHealth = 100f;

    private float currentHealth;

    [Header("UI")]
   // [SerializeField]
  //  private Slider healthSlider;

    [SerializeField]
    private Image fillImage;

    private void Start()
    {
        currentHealth = maxHealth;

        UpdateHealthUI();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        currentHealth =
            Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        float healthPercent =
            currentHealth / maxHealth;

     //   healthSlider.value = healthPercent;

        fillImage.color =
            Color.Lerp(Color.red, Color.green, healthPercent);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}