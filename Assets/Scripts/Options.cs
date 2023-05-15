using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;

    [SerializeField] private Toggle muteToggle;

    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        muteToggle.isOn = audioManager.IsMute;
        bgmSlider.value = audioManager.BgmVolume;
        sfxSlider.value = audioManager.SfxVolume;
    }
}
