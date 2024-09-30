using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;
    private AudioSource BGM;
    private AudioSource audioSource;

    [Header("Audio Clips")]
    [SerializeField] AudioClip titleMusic;
    [SerializeField] AudioClip settingMusic;
    [SerializeField] AudioClip playMusic;
    [SerializeField] AudioClip gameWin;
    [SerializeField] AudioClip gameLose;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
