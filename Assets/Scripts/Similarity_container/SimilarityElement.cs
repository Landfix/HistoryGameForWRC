using System;
using Similarity_container.Configs;
using UnityEditorInternal;
using UnityEngine;

namespace Similarity_container
{
    
    [RequireComponent(typeof(LineRenderer))]
    public class SimilarityElement : MonoBehaviour
    {
        [Header("Visual")]
        [SerializeField] private SpriteRenderer _model;
        [SerializeField] private SpriteRenderer _selectableAreaRenderer;
        [Header("Logic")]
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField,Range(0,1)] private float _resolution = 0.1f;
        [SerializeField] private Collider2D _collider2D;

        private SimilarityType _similarityType;
        private SideType _sideType;
        
        private Camera _camera;
        private Vector2 _startPos;
        private bool _isMouseDrag;

        public SideType SideType => _sideType;

        public SimilarityType SimilarityType => _similarityType;
        public Collider2D Collider2D => _collider2D;

        public event Action<SimilarityElement,Vector2> Up;
        
        public void Initialize(Camera camera, SimilarityElementConfig similarityElementConfig)
        {
            _camera = camera;
            SetData(similarityElementConfig);
            SwitchSelectableArea(false);
            
            var position = transform.position;
            _lineRenderer.SetPosition(0,position);
            _lineRenderer.SetPosition(1,position);
            _startPos = position;
        }

        private void SetData(SimilarityElementConfig config)
        {
            _similarityType = config.SimilarityType;
            _sideType = config.SideType;
            _model.sprite = config.Sprite;
        }

        private void OnMouseDown()
        {
            SwitchSelectableArea(true);
        }

        private void OnMouseDrag()
        {
            _isMouseDrag = true;
            SetPosition(GetMousePosition());
        }

        private void OnMouseUp()
        {
            SwitchSelectableArea(false);
            if (_isMouseDrag)
            {
                Up?.Invoke(this,GetMousePosition());
                //_collider2D.offset = GetMousePosition();
                _isMouseDrag = false;
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
            //_collider2D.offset = pos - _startPos;
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

        public void ResetPosition()
        {
            //incorrected
            SetPosition(_startPos);
        }

        public void SelectCorrectArea()
        {
            SwitchSelectableArea(true);
            _collider2D.enabled = false;
        }

        private void SwitchSelectableArea(bool isActivate) => 
            _selectableAreaRenderer.enabled = isActivate;
    }
}