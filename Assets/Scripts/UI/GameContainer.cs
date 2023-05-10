using Systems;
using AssemblyOfComputer_container;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class GameContainer : MonoBehaviour
    {
        private const string MenuScene = "Menu";
        
        [SerializeField] private Button _menuBtn;
        [SerializeField] private Button _hintBtn;
        [SerializeField] private Button _skipBtn;

        private BaseBootstrapper _baseBootstrapper;

        public void Initialize(BaseBootstrapper baseBootstrapper)
        {
            _baseBootstrapper = baseBootstrapper;
            _menuBtn.onClick.AddListener(OnClickMenuScene);
            _skipBtn.onClick.AddListener(OnClickSkipScene);
            _hintBtn.onClick.AddListener(OnClickShowHint);
        }

        private void OnClickShowHint()
        {
            _baseBootstrapper.ShowHint();
        }

        private void OnClickSkipScene()
        {
            _skipBtn.onClick.RemoveListener(OnClickSkipScene);
            GlobalManager.I.LevelSkip();
        }

        private void OnClickMenuScene()
        {
            _menuBtn.onClick.RemoveListener(OnClickMenuScene);
            SceneManager.LoadScene(MenuScene);
        }
    }
}