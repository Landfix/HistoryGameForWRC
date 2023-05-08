using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Systems
{
    [Serializable]
    public class Preferences
    {
        public int level = 1;

        public int soundPlayback = 1;
        public int adsSoundPlayback = -1;
        public int rewardReplay = 1;

        const string PreferencesKey = "preferences";

        bool _isInitialized = false;

        public int MaxNumberOfLevels { get; private set; }

        public event Action<bool> TurnedOffSoundToAds;
        public event Action<bool> SwitchedSoundByButton;

        public void Init()
        {
            if (_isInitialized) return;
            
            MaxNumberOfLevels = SceneManager.sceneCountInBuildSettings - 1;

            string prefsString = PlayerPrefs.GetString(PreferencesKey, null);
            //string prefsString = GameScore.GS_Player.GetString(PreferencesKey);

            if (string.IsNullOrEmpty(prefsString))
            {
                SetDefaultPrefs();
            }
            else
            {
                try
                {
                    Copy(JsonUtility.FromJson<Preferences>(prefsString));
                }
                catch
                {
                    Debug.LogError($"Invalid preferences string format: {prefsString}");
                    PlayerPrefs.DeleteKey(PreferencesKey);
                    SetDefaultPrefs();
                }
            }

            _isInitialized = true;
        }

        public void Copy(Preferences other)
        {
            level = other.level;
            soundPlayback = other.soundPlayback;
            adsSoundPlayback = other.adsSoundPlayback;
            rewardReplay = other.rewardReplay;
        }

        public void SetDefaultPrefs()
        {
            Copy(new Preferences());
            SavePreferences();
        }

        public void SavePreferences()
        {
            string preferencesString = JsonUtility.ToJson(this);
            PlayerPrefs.SetString(PreferencesKey, preferencesString);
            
            // todo recast
            // GameScore.GS_Player.Set(PreferencesKey,preferencesString);
            // GameScore.GS_Player.Sync();
        }

        public void SetLevel(int value)
        {
            level += value;

            if (level > MaxNumberOfLevels)
                level = MaxNumberOfLevels;

            SavePreferences();
        }

        public void ResetAllLevels()
        {
            level = 1;
            SavePreferences();
        }
        
        public void SetSoundPlayback(int value, bool isAds = false)
        {
            if (isAds)
            {
                adsSoundPlayback = value;
                TurnedOffSoundToAds?.Invoke(adsSoundPlayback == 1);
            }
            else
            {
                soundPlayback = value;
                SwitchedSoundByButton?.Invoke(soundPlayback == 1);
            }

            SavePreferences();
        }

        public void SetRewardReplay(int value)
        {
            rewardReplay = value;
            SavePreferences();
        }
    }
}