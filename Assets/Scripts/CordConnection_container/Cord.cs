using System;
using UnityEngine;

namespace WireConnection_container
{
    [RequireComponent(typeof(LineRenderer))]
    public class Cord : MonoBehaviour
    {
        [SerializeField] private Color _color;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Collider2D _collider2D;

        private Camera _camera;
        private CordTextBlock _cordTextBlock;
        private float _resolution = 0.17f;
        private Vector2 _startPos;

        private bool _isUp = true;

        public bool IsConnect { get; private set; }

        public event Action Connected;

        public void Initialize(CordTextBlock cordTextBlock, Camera camera)
        {
            _camera = camera;
            _cordTextBlock = cordTextBlock;
            _lineRenderer.material.color = _color;
            
            var position = transform.position;
            _lineRenderer.SetPosition(0,position);
            _lineRenderer.SetPosition(1,position);
            _startPos = position;
        }

        private void OnMouseDown()
        {
            if(IsConnect)
                return;
            
            if (_collider2D.OverlapPoint(GetMousePosition()))
            {
                _isUp = false;
            }
        }

        private void OnMouseDrag()
        {
            if(_isUp || IsConnect)
                return;
                
            SetPosition(GetMousePosition());
        }

        private void OnMouseUp()
        {
            if(IsConnect)
                return;
            
            _isUp = true;
            
            if (_cordTextBlock.CheckConnect(GetMousePosition()))
            {
                _collider2D.enabled = false;
                IsConnect = true;
                Connected?.Invoke();
                _collider2D.offset = GetMousePosition();
            }
            else
            {
                SetPosition(_startPos);
            }
        }

        private void SetPosition(Vector2 pos)
        {
            if (!CanAppend(pos))
                return;
            
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, pos);
            _collider2D.offset = pos - _startPos;
        }
        
        private bool CanAppend(Vector2 pos)
        {
            if (_lineRenderer.positionCount == 0) return true;

            return Vector2.Distance(_lineRenderer.GetPosition(_lineRenderer.positionCount - 1), pos) > _resolution;
        }

        private Vector3 GetMousePosition()
        {
            return _camera.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}