using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MenuUi : MonoBehaviour
    {
        [SerializeField] private Button _playBtn;

        private void Start()
        {
            _playBtn.onClick.AddListener(OnClickPlay);
        }

        private void OnClickPlay()
        {
            
        }
    }
}