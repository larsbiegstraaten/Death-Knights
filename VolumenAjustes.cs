using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumenAjustes : MonoBehaviour
{
    public Slider slider;
    public float slidervalue;
    public Image imageMute;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("VolumeAudio", 0.5f);
        AudioListener.volume = slider.value;
        ComprobarMute();

    }

    public void ChangeSlider(float valor)
    {
        slidervalue = valor;
        PlayerPrefs.SetFloat("VolumeAudio", slidervalue);
        AudioListener.volume = slider.value;
        ComprobarMute();

    }

    public void ComprobarMute()
    {
        if (slidervalue == 0)
        {
            imageMute.enabled = true;
        }
        else
        {
            imageMute.enabled = false;
        }
    }
}
