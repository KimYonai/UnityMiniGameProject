using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingScene : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider BGMSlider;	
    [SerializeField] private Toggle BGMMute;
    [SerializeField] GameObject settingBGM;


    private void Awake()
    {
        BGMSlider.onValueChanged.AddListener(SetBGMVolume);
        BGMMute.onValueChanged.AddListener(SetBGMMute);
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            BGMSlider.value = PlayerPrefs.GetFloat("Volume");
        }
        else
            BGMSlider.value = 0.5f;

        audioMixer.SetFloat("Master", Mathf.Log10(BGMSlider.value) * 20);

        settingBGM.SetActive(true);
    }

    // Slider를 통해 걸어놓은 이벤트
    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Volume", BGMSlider.value);
    }

    private void SetBGMMute(bool mute)
    {
        AudioListener.volume = (mute ? 0 : 1);
    }

    public void BackToLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}
