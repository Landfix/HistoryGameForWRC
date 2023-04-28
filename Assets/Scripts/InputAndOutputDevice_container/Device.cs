using System;
using UnityEngine;

namespace InputAndOutputDevice_container
{
    public class Device : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _selectedModel;

        private bool _isSelected;

        public bool IsSelected => _isSelected;
        
        public event Action Selected;

        public void Initialize()
        {
            _selectedModel.enabled = false;
            _isSelected = false;
        }

        private void OnMouseUp()
        {
            if (_isSelected)
            {
                _selectedModel.enabled = false;
                _isSelected = false;
            }
            else
            {
                _selectedModel.enabled = true;
                _isSelected = true;
                Selected?.Invoke();
            }
        }
    }
}