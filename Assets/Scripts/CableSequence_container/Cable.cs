using System;
using LineSequence_container.Configs;
using UnityEngine;

namespace LineSequence_container
{
    public class Cable : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _model;
        
        private CableType _cableType;
        private bool _isConnect;

        private Camera _camera;
        private Vector2 _startPos;
        private Vector2 _dragOffset;

        public CableType CableType => _cableType;

        public event Action<Cable> Switched;

        public void Initialize(Camera camera, CableBlock cableBlock, CableConfig cableConfig)
        {
            var position = cableBlock.transform.position;
            _startPos = position;
            transform.position = position;

            _camera = camera;
            _model.sprite = cableConfig.Model;
            _cableType = cableConfig.CableType;
        }
        
        private void OnMouseDown()
        {
            _dragOffset = (Vector2)transform.position - GetMousePosition();
        }

        private void OnMouseDrag()
        {
            transform.position = GetMousePosition() + _dragOffset;
        }

        private void OnMouseUp()
        {
            transform.position = _startPos;
        }

        private Vector2 GetMousePosition()
        {
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            return mousePosition;
        }
    }
}