using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    [SerializeField] private string whichMixer = "MusicVol";

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat(whichMixer, Mathf.Log10(sliderValue) * 20);
    }
}
