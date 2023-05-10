using Systems;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class WinCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _correctIconTransform;
        
        private BaseBootstrapper _baseBootstrapper;
        
        public void Initialize(BaseBootstrapper baseBootstrapper)
        {
            _baseBootstrapper = baseBootstrapper;
            _baseBootstrapper.Won += Won;
            SharpHideCurtain();
        }

        private void Won()
        {
            _baseBootstrapper.Won -= Won;
            ShowCurtain();
            _correctIconTransform.DOScale(Vector2.one * 3, 0.4f).SetEase(Ease.Flash)
                .OnComplete(GlobalManager.I.LevelSkip);
        }

        private void ShowCurtain()
        {
            _canvasGroup.DOFade(1f, 0.5f).SetEase(Ease.Flash);
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public void HideCurtain()
        {
            _canvasGroup.DOFade(0f, 1f).SetEase(Ease.Flash);
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        private void SharpHideCurtain()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}