using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    
    public void SetHealthPercentage(float value)
    {
        if (value > 1)
        {
            value = 1.0f;
        }
        else if (value < 0)
        {
            value = 0.0f;
        }
        healthSlider.value = value;
    }
}
