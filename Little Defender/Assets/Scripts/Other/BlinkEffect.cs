using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEffect : MonoBehaviour
{
    [Header("Enemy Blink Effect")]
    [ColorUsage(true, true)]
    [SerializeField] private Color blinkColor = Color.white;
    [SerializeField] private float blinkTime = 0.25f;
    [SerializeField] private AnimationCurve blinkSpeedCurve;

    private SpriteRenderer spriteRenderer;
    private Material material;

    private Coroutine damageBlinkCoroutine;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        Initialize();
    }

    private void Initialize()
    {
        material = spriteRenderer.material;
    }

    public void DamageEffect()
    {
        damageBlinkCoroutine = StartCoroutine(DamageBlink());
    }

    private IEnumerator DamageBlink()
    {
        SetBlinkColor();

        float currentBlinkAmount = 0f;
        float elapsedTime = 0f;
        while (elapsedTime < blinkTime)
        {
            elapsedTime += Time.deltaTime;
            currentBlinkAmount = Mathf.Lerp(1f, blinkSpeedCurve.Evaluate(elapsedTime), (elapsedTime / blinkTime));
            SetBlinkAmount(currentBlinkAmount);

            yield return null;
        }
    }

    private void SetBlinkColor()
    {
        material.SetColor("_BlinkColor", blinkColor);
    }

    private void SetBlinkAmount(float amount)
    {
        material.SetFloat("_BlinkAmount", amount);
    }
}
