using UnityEngine;
using UnityEngine.UI;

public class BarraVelocidad : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void Initialize(float min, float max)
    {
        slider.minValue = min;
        slider.maxValue = max;
    }

    public void SetVelocity(float value)
    {
        slider.value = value;
        Image fill = slider.fillRect.GetComponent<Image>();
        float normalizedValue = (slider.value - slider.minValue) / (slider.maxValue - slider.minValue);
    }
}