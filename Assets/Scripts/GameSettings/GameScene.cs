using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Toggle BGMMute;
    [SerializeField] GameObject pauseWindow;
    [SerializeField] GameObject boss;
    [SerializeField] GameObject gameClear;
    [SerializeField] bool isPause;
    [SerializeField] GameManager gameManager;

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
        {
            BGMSlider.value = 0.5f;
        }

        gameClear.SetActive(false);

        audioMixer.SetFloat("Master", Mathf.Log10(BGMSlider.value) * 20);

        pauseWindow.SetActive(false);
        isPause = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GamePause();
        }

        if (boss == null)
        {
            gameClear.SetActive(true);
        }
    }

    public void GamePause()
    {
        if (isPause == false)
        {
            Time.timeScale = 0;
            isPause = true;
            pauseWindow.SetActive(true);
            return;
        }
        else
        {
            Time.timeScale = 1;
            isPause = false;
            pauseWindow.SetActive(false);
            return;
        }
    }

    public void BackToLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Volume", BGMSlider.value);
    }

    private void SetBGMMute(bool mute)
    {
        AudioListener.volume = (mute ? 0 : 1);
    }
}
