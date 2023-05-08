using System;
using UnityEngine;

namespace AssemblyOfComputer_container
{
    public class ComputerPart : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider2D;
    
        private Camera _camera;
        private ComputerShadowPart _computerShadowPart;
        private Vector2 _dragOffset;
        private Vector2 _startPos;
        private bool _isConnect;

        public bool IsConnect => _isConnect;

        public event Action Connected;
    
        public void Initialize(Camera camera, ComputerShadowPart computerShadowPart)
        {
            _startPos = transform.position;
            _camera = camera;
            _computerShadowPart = computerShadowPart;
        }

        private void OnMouseDown()
        {
            if(_isConnect)
                return;
        
            _dragOffset = (Vector2)transform.position - GetMousePosition();
        }

        private void OnMouseDrag()
        {
            if(_isConnect)
                return;
        
            transform.position = GetMousePosition() + _dragOffset;
        }

        private void OnMouseUp()
        {
            if(_isConnect)
                return;
        
            if (_computerShadowPart.Collider2D.OverlapPoint(GetMousePosition()))
            {
                transform.position = _computerShadowPart.transform.position;
                _isConnect = true;
                Connected?.Invoke();
            }
            else
            {
                transform.position = _startPos;
            }
        }

        private Vector2 GetMousePosition()
        {
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            return mousePosition;
        }
    }
}