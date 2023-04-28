using System;
using UnityEngine;

namespace InputAndOutputDevice_container
{
    public class FakeDevice : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _selectedModel;

        private bool _isSelected;

        public bool IsSelected => _isSelected;

        public event Action Unselected;

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
                Unselected?.Invoke();
            }
            else
            {
                _selectedModel.enabled = true;
                _isSelected = true;
            }
        }
    }
}