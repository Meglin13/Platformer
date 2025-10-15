using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Slider healthSlider;

    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Money")]
    [SerializeField] private TextMeshProUGUI moneyText;

    [Header("References")]
    [SerializeField] private Player player;

    private void Start()
    {
        if (player != null)
        {
            UpdateHealthUI();
            UpdateMoneyUI();

            player.OnTakeDamage += UpdateHealthUI;
            player.Money.OnValueChanged += UpdateMoneyUI;
        }
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = player.Health.CurrentValue / (float)player.Health.MaxValue;
        }

        if (healthText != null)
        {
            healthText.text = $"{player.Health.CurrentValue}/{player.Health.MaxValue}";
        }
    }

    private void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = player.Money.CurrentValue.ToString();
        }
    }

    private void OnDestroy()
    {
        if (player != null)
        {
            player.OnTakeDamage -= UpdateHealthUI;
            player.Money.OnValueChanged -= UpdateMoneyUI;
        }
    }
}