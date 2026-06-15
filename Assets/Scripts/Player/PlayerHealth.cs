using TMPro;
using UnityEngine;


public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("UI")]
    //[SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip damageSFX;


    [SerializeField]
    private PauseMenu pauseMenu;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    private void Update()
    {


        //  Debug.Log($"Health: {currentHealth}/{maxHealth}");
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Player Damage: " + damage);

        audioSource.PlayOneShot(damageSFX);

        currentHealth -= damage;
        DmgFlash.Instance.ShowFlash();

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

        healthText.text =
            Mathf.RoundToInt(currentHealth).ToString();

        Color healthColor = Color.Lerp(Color.red, Color.green, healthPercent);

        healthText.color = healthColor;
        //   fillImage.color = healthColor;
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

        pauseMenu.ShowPanel(gameOverPanel);

        EnemyMelee[] melee = FindObjectsByType<EnemyMelee>(FindObjectsSortMode.None);

        AudioSource[] audios = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

        foreach (AudioSource audio in audios)
        {
            audio.Stop();
        }

        foreach (var enemy in melee)
        {
            Destroy(enemy.gameObject);
        }


        Time.timeScale = 0f;
    }
}
