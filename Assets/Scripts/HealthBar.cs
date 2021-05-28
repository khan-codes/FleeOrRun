using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;
    float fillAlpha = 167f;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        fill.color =  new Vector4(gradient.Evaluate(1f).r, 
                                  gradient.Evaluate(1f).g, 
                                  gradient.Evaluate(1f).b, 
                                  fillAlpha);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSlider(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        fill.color = new Vector4(gradient.Evaluate(slider.normalizedValue).r, 
                                 gradient.Evaluate(slider.normalizedValue).g, 
                                 gradient.Evaluate(slider.normalizedValue).b, 
                                 fillAlpha);
    }
}
