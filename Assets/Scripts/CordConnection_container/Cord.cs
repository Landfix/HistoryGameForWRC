using System;
using System.Collections.Generic;
using UnityEngine;

namespace WireConnection_container
{
    [RequireComponent(typeof(LineRenderer))]
    public class Cord : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Collider2D _collider2D;

        private Camera _camera;
        private float _resolution = 0.17f;
        private Vector2 _startPos;

        private bool _isUp;

        public void Initialize(Camera camera)
        {
            _camera = camera;
            _startPos = _lineRenderer.GetPosition(_lineRenderer.positionCount - 1);
        }

        // private void OnMouseDown()
        // {
        //     _dragOffset = (Vector2)transform.position - GetMousePosition();
        // }

        private void Update()
        {
            Vector2 mousePos = GetMousePosition();
            if (Input.GetMouseButtonDown(0))
            {
                
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                if(_isUp)
                    return;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isUp = true;
            }
        }

        private void OnMouseDrag()
        {
            _collider2D.offset = GetLastPosition();
            if (_collider2D.OverlapPoint(GetMousePosition()))
            {
                SetPosition(GetMousePosition());
            }
        }
        
        private void SetPosition(Vector2 pos)
        {
            if (!CanAppend(pos))
                return;
            
            _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, pos);
            _collider2D.offset = pos;
        }
        
        private bool CanAppend(Vector2 pos)
        {
            if (_lineRenderer.positionCount == 0) return true;

            return Vector2.Distance(_lineRenderer.GetPosition(_lineRenderer.positionCount - 1), pos) > _resolution;
        }

        private Vector2 GetLastPosition()
        {
            return _lineRenderer.GetPosition(_lineRenderer.positionCount - 1);
        }

        private Vector3 GetMousePosition()
        {
            return _camera.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}