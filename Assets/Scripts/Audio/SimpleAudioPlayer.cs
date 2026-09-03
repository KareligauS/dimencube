using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class SimpleAudioPlayer : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _sfxDropdown;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _stopButton;

    [Inject] private readonly IAudioManager _audioManager;

    private void Awake()
    {
        _sfxDropdown.ClearOptions();
        _sfxDropdown.AddOptions(new List<string>(Enum.GetNames(typeof(AudioClipEnum))));

        _playButton.onClick.AddListener(PlaySelectedSfx);
        _stopButton.onClick.AddListener(StopSfx);
    }

    private void PlaySelectedSfx()
    {
        _audioManager.PlaySFX((AudioClipEnum)_sfxDropdown.value);
    }

    private void StopSfx()
    {
        _audioManager.StopSFX();
    }
}
