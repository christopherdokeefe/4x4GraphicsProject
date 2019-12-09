using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerSliderBehavior : MonoBehaviour
{
    private Slider slider;
    private float sliderAdjustValue = 5f;

    // Start is called before the first frame update
    void Start()
    {
        slider = transform.GetComponent<Slider>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            slider.value -= sliderAdjustValue;
        }

        if (Input.GetKey(KeyCode.E))
        {
            slider.value += sliderAdjustValue;
        }
    }
}
