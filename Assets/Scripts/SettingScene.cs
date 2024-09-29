using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingScene : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] AudioMixer masterVolume;
    [SerializeField] TextMeshProUGUI volumeText;
    [SerializeField] StringBuilder volumeSB = new StringBuilder();

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

        volumeSB.Clear();
        volumeSB.Append(volume);
        volumeText.SetText(volumeSB);
    }
}
