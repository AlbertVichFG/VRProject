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

    private void Update()
    {
        // Aquí podríem afegir regeneració de vida o altres mecanismes relacionats amb la salut.

        Debug.Log($"Health: {currentHealth}/{maxHealth}");
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
        float healthPercent = currentHealth / maxHealth;

        healthSlider.value = healthPercent;

        fillImage.color = Color.Lerp(Color.red, Color.green, healthPercent);
    }

    public void Knockback(Vector3 direction, float force)
    {
        CharacterController cc = GetComponent<CharacterController>();

        if (cc != null)
        {
            cc.Move(direction * force);
        }
    }

    void Die()
    {
        Debug.Log("Player Dead");

        // Aquí després:
        // SceneManager.LoadScene("GameOver");
    }
}
