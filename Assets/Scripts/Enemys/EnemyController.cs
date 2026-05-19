using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [Header("Stats")]
    public float maxHealth = 100f;
    protected float currentHealth;

    [Header("Movement")]
    public float stoppingDistance = 2f;

    [Header("References")]
    public Transform player;

    protected NavMeshAgent agent;

    /*
    [Header("UI")]
    [SerializeField]
    protected Slider healthSlider;

    [SerializeField]
    protected Image fillImage;
    */

    protected virtual void Start()
    {
        currentHealth = maxHealth;

        agent = GetComponent<NavMeshAgent>();

        agent.stoppingDistance = stoppingDistance;

        //UpdateHealthUI();
    }

    protected virtual void Update()
    {
        if (player == null)
            return;

        agent.SetDestination(player.position);

        //LookAtPlayer();
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;

        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        //UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    /*
    void UpdateHealthUI()
    {
        float healthPercent = currentHealth / maxHealth;

        healthSlider.value = healthPercent;

        fillImage.color = Color.Lerp(Color.red, Color.green, healthPercent);
    }

    void LookAtPlayer()
    {
        Canvas canvas = healthSlider.GetComponentInParent<Canvas>();

        canvas.transform.LookAt(Camera.main.transform);
    }
    */
}