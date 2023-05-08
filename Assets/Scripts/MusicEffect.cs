using Systems;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicEffect : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    
    private bool _isTurnOnCurrentMusic;
    private Preferences _preferences;

    public void Initialize(Preferences preferences)
    {
        _preferences = preferences;
        _isTurnOnCurrentMusic = _preferences.soundPlayback == 1;
            
        if (_isTurnOnCurrentMusic)
            _audioSource.Play();
        else
            _audioSource.Stop();

        _preferences.TurnedOffSoundToAds += TurnedOffSoundToAds;
        _preferences.SwitchedSoundByButton += SwitchedSoundByButton;
    }
    
    private void SwitchedSoundByButton(bool isActive) =>
        _isTurnOnCurrentMusic = isActive;

    private void TurnedOffSoundToAds(bool isActive)
    {
        if (isActive)
        {
            if (_isTurnOnCurrentMusic) 
                _audioSource.Play();
        }
        else
            _audioSource.Stop();

    }

    public void IsPlaybackMusic(bool isActive)
    {
        if (isActive)
            _audioSource.Play();
        else
            _audioSource.Stop();
    }

    private void OnDestroy()
    {
        _preferences.TurnedOffSoundToAds -= TurnedOffSoundToAds;
        _preferences.SwitchedSoundByButton -= SwitchedSoundByButton;
    }
}