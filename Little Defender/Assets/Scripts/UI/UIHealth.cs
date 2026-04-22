using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    [Header("UI Settings")]
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public TMP_Text healthText;

    [Header("Ease Settings")]
    [SerializeField] private float easeSpeed = 2.0f;

    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
        if (health != null)
        {
            health.onHealthChanged += UpdateHealthUI;
        }
    }

    private void Start()
    {
        healthSlider.maxValue = health.MaxHealth;
        healthSlider.value = health.CurrentHealth;

        easeHealthSlider.maxValue = health.MaxHealth;
        easeHealthSlider.value = health.CurrentHealth;
    }

    private void UpdateHealthUI(float currentHealth, float maxHealth)
    {
        healthSlider.value = currentHealth;
        healthText.text = $"{currentHealth}/{maxHealth}";
        StartCoroutine(EaseHealthBar(currentHealth));
    }

    private IEnumerator EaseHealthBar(float targetHealth)
    {
        while (easeHealthSlider.value > targetHealth)
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, targetHealth, easeSpeed * Time.deltaTime);
            yield return null;
        }

        easeHealthSlider.value = targetHealth;
    }

    private void OnDestroy()
    {
        health.onHealthChanged -= UpdateHealthUI;
    }
}
