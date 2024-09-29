using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingScene : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] AudioMixer masterVolume;

    public void VolumeControl()
    {
        float volume = volumeSlider.value;

        if (volume == -40f)
        {
            masterVolume.SetFloat(name, -80);
        }
        else
        {
            masterVolume.SetFloat(name, volume);
        }
    }
}
