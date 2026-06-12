using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("UI")]
    [SerializeField]private Image fillImage;
    [SerializeField]private TextMeshProUGUI healthText;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    private void Update()
    {
        // Aquí podríem afegir regeneració de vida o altres mecanismes relacionats amb la salut.

      //  Debug.Log($"Health: {currentHealth}/{maxHealth}");
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Player Damage: " + damage);

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
        float healthPercent =
            currentHealth / maxHealth;

        healthText.text =
            Mathf.RoundToInt(currentHealth).ToString();

        Color healthColor =
            Color.Lerp(
                Color.red,
                Color.green,
                healthPercent
            );

        healthText.color = healthColor;
        fillImage.color = healthColor;
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
        // Saltar GameOver
    }
}
