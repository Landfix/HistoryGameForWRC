using System;
using Systems;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffect : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private bool _isTurnOnCurrentSound;
    private Preferences _cachedPreferences;

    private void Start()
    {
        _cachedPreferences = GlobalManager.I.Preferences;
        _isTurnOnCurrentSound = _cachedPreferences.soundPlayback == 1;
            
        CheckSound();
            
        _cachedPreferences.TurnedOffSoundToAds += TurnedOffSoundToAds;
        _cachedPreferences.SwitchedSoundByButton += SwitchedSoundByButton;
    }

    private void SwitchedSoundByButton(bool isActive) =>
        _isTurnOnCurrentSound = isActive;

    private void TurnedOffSoundToAds(bool isActive)
    {
        if (isActive)
        { 
            if(_isTurnOnCurrentSound)
                _audioSource.enabled = true;
        }
        else
            _audioSource.enabled = false;
    }

    public void PlayClip()
    {
        if (_audioSource.isActiveAndEnabled)
            _audioSource.Play();
    }

    public void StopPlay()
    {
        if (_audioSource)
            _audioSource.Stop();
    }

    private void OnDestroy()
    {
        _cachedPreferences.TurnedOffSoundToAds -= TurnedOffSoundToAds;
        _cachedPreferences.SwitchedSoundByButton -= SwitchedSoundByButton;
    }

    private void CheckSound() =>
        _audioSource.enabled = _cachedPreferences.soundPlayback == 1;
}