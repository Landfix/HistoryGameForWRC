using System;
using Systems;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MenuUi : MonoBehaviour
    {
        private const string SceneName = "Level {0}";
        
        [SerializeField] private Button _playBtn;

        private Preferences _cachedPreferences;

        private void Start()
        {
            _cachedPreferences = GlobalManager.I.Preferences;
            _playBtn.onClick.AddListener(OnClickPlay);
        }

        private void OnClickPlay()
        {
            _playBtn.onClick.RemoveListener(OnClickPlay);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}