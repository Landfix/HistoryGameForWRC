using System;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class IncorrectCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _correctIconTransform;
        
        private BaseBootstrapper _baseBootstrapper;
        
        public void Initialize(BaseBootstrapper baseBootstrapper)
        {
            _baseBootstrapper = baseBootstrapper;
            _baseBootstrapper.Incorrected += ShowCurtain;
            SharpHideCurtain();
        }

        private void ShowCurtain()
        {
            _canvasGroup.DOFade(1f, 0.3f).SetEase(Ease.Flash).OnComplete(HideCurtain);
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        private void HideCurtain()
        {
            _canvasGroup.DOFade(0f, 0.3f).SetEase(Ease.Flash);
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        private void SharpHideCurtain()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }


        private void OnDestroy()
        {
            _baseBootstrapper.Incorrected -= ShowCurtain;
        }
    }
}