using System;
using Localization_container;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Systems
{
    public class GlobalManager : SingletonMono<GlobalManager>
    {
        private const string LevelScene = "Level {0}";
        private const string BonusLevelScene = "BonusLevel {0}";
        
        [SerializeField] private MusicEffect _musicEffect;
        [SerializeField] private LanguageManager _languageManager;

        private readonly Preferences _preferences = new Preferences();
        
        public Preferences Preferences => _preferences;
        public MusicEffect MusicEffect => _musicEffect;

        public override void Awake()
        {
            base.Awake();
            // GameScore.GS_SDK.OnReady += OnReady;
            // GameScore.GS_Player.OnPlayerReady += OnPlayerReady;
            // GameScore.GS_Game.OnPause += OnPause;
            // GameScore.GS_Game.OnResume += OnResume;
            
            // todo delete
            _preferences.Init();
            _musicEffect.Initialize(_preferences);
        }

        private void OnPlayerReady()
        {
            _preferences.Init();
            _musicEffect.Initialize(_preferences);
        }

        private void OnResume() => 
            _preferences.SetSoundPlayback(1, true);

        private void OnPause() => 
            _preferences.SetSoundPlayback(-1, true);

        private void OnReady() => 
            _languageManager.Init();

        public void LevelSkip()
        {
            _preferences.SetLevel(1);
            Resources.UnloadUnusedAssets();
            SceneManager.LoadScene(string.Format(LevelScene,_preferences.level));
        }

        public void GetNewCompleteCharacter(int skinType, int numberOfAdViews)
        {
            // _preferences.SetValueSkinByType((CompleteCharacterType)skinType,1);
            //
            // if (_preferences.GetValueSkinByType((CompleteCharacterType) skinType) >= numberOfAdViews)
            //     _preferences.UseANewSetOfCharacters(skinType);
        }

        private void OnDestroy()
        {
            // GameScore.GS_SDK.OnReady -= OnReady;
            // GameScore.GS_Player.OnPlayerReady -= OnPlayerReady;
            // GameScore.GS_Game.OnPause -= OnPause;
            // GameScore.GS_Game.OnResume -= OnResume;
        }

    }
}