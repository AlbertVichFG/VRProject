using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("UI")]
    [SerializeField]
    private Slider healthSlider;

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

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        healthSlider.value = currentHealth / maxHealth;

        // Color segons percentatge
        if (currentHealth > maxHealth * 0.6f)
        {
            fillImage.color = Color.green;
        }
        else if (currentHealth > maxHealth * 0.3f)
        {
            fillImage.color = Color.yellow;
        }
        else
        {
            fillImage.color = Color.red;
        }
    }

    void Die()
    {
        Debug.Log("Player Dead");

        // Aquí després:
        // SceneManager.LoadScene("GameOver");
    }
}
