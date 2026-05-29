using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]
    protected float maxHealth = 100f;

    protected float currentHealth;

    [Header("UI")]
    //[SerializeField]
    //protected Slider healthSlider;

    [SerializeField]
    protected Image fillImage;

    protected virtual void Start()
    {
        currentHealth = maxHealth;

        UpdateHealthUI();
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void UpdateHealthUI()
    {
        float healthPercent = currentHealth / maxHealth;

        //healthSlider.value = healthPercent;

        fillImage.color = Color.Lerp(Color.red, Color.green, healthPercent);
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}