using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueScript : MonoBehaviour
{
    private Slider thisSlide;
    [SerializeField] private Slider otherSlide;
    [SerializeField] private string whichSlider = "musicSlide";

    // Start is called before the first frame update
    void Start()
    {
        thisSlide = GetComponent<Slider>();
        thisSlide.value = PlayerPrefs.GetFloat(whichSlider, 1f);
        otherSlide.minValue = thisSlide.minValue;
        otherSlide.maxValue = thisSlide.maxValue;
        otherSlide.value = thisSlide.value;
    }

    // Update is called once per frame
    void Update()
    {
        if (otherSlide.value != thisSlide.value)
        {
            otherSlide.value = thisSlide.value;
            PlayerPrefs.SetFloat(whichSlider, thisSlide.value);
        }
    }
}
