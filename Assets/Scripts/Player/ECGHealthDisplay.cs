using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ECGHealthDisplay : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;

    [SerializeField] private Image ecgImage;

    [SerializeField] private TextMeshProUGUI hpText;

    private RectTransform rect;

    private float offset;

    private void Start()
    {
        rect = ecgImage.rectTransform;
    }

    private void Update()
    {
        float healthPercent =
            playerHealth.currentHealth /
            playerHealth.maxHealth;

        hpText.text =
            Mathf.RoundToInt(playerHealth.currentHealth) + " HP";

        ecgImage.color = Color.Lerp(Color.red, Color.cyan, healthPercent);

        float speed = Mathf.Lerp(400f, 100f, healthPercent);

        offset += speed * Time.deltaTime;

        rect.anchoredPosition =
            new Vector2(-offset, rect.anchoredPosition.y);

        if (offset > rect.rect.width)
        {
            offset = 0;
        }
    }
}
