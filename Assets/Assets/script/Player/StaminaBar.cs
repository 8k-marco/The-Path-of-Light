using System;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public float maxStamina = 100f;
    public float regenRate = 10f;
    public Swinging swinging;
    public Slider staminaSlider;

    private float currentStamina;
    public bool CanMakeSpecialMove => currentStamina >= maxStamina;

    public void Add(float stamina)
    {
        if (stamina < 0) return;
        currentStamina += stamina;
        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
    }

    private void Start()
    {
        currentStamina = maxStamina;
    }

    private void Update()
    {
        //Regenerate();
        UpdateStaminaUI();
    }

    private void Regenerate()
    {
        currentStamina += regenRate * Time.deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
    }

    public void EmptyStamina()
    {
        this.currentStamina = 0;
    }

    private void UpdateStaminaUI()
    {
        if (staminaSlider != null)
            staminaSlider.value = (currentStamina / maxStamina);
    }
}
