using System;
using System.Collections.Generic;
using DG.Tweening;
using LineSequence_container.Configs;
using UnityEngine;

namespace LineSequence_container
{
    public class Cable : MonoBehaviour
    {
        private const int MinSortingOrder = 2;
        private const int MaxSortingOrder = 10;
        
        [SerializeField] private SpriteRenderer _model;
        [SerializeField] private Collider2D _collider2D;
        
        private CableType _cableType;
        private Vector2 _savedPosition;
        private Vector2 _startPos;
        private Vector2 _dragOffset;
        private int _index; 
        private bool _isMouseDrag;

        private Camera _camera;
        private CableBlock _cableBlock;

        public Collider2D Collider2D => _collider2D;
        public Vector2 StartPos => _startPos;
        public bool IsCorrectIndex => _index == (int)_cableType;

        public event Action<Cable, Vector2> Up;

        public void Initialize(Camera camera, CableBlock cableBlock, CableConfig cableConfig, int index)
        {
            _index = index;
            _cableBlock = cableBlock;
            var position = cableBlock.transform.position;
            
            _startPos = position;
            transform.position = position;

            _camera = camera;
            _model.sprite = cableConfig.Model;
            _cableType = cableConfig.CableType;
        }

        private void OnMouseDown()
        {
            _model.sortingOrder = MaxSortingOrder;
            _dragOffset = (Vector2)transform.position - GetMousePosition();
        }

        private void OnMouseDrag()
        {
            _isMouseDrag = true;
            transform.position = GetMousePosition() + _dragOffset;
        }

        private void OnMouseUp()
        {
            if (_isMouseDrag)
            {
                Up?.Invoke(this, GetMousePosition());
                _isMouseDrag = false;
            }
            else
                transform.position = _startPos;
            
            _model.sortingOrder = MinSortingOrder;
        }

        private Vector2 GetMousePosition()
        {
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            return mousePosition;
        }

        public void SetNewBlock(CableBlock cableBlock, int index)
        {
            _index = index;
            _startPos = _cableBlock.transform.position;
            _cableBlock = cableBlock;
        }

        public void Move(Vector2 pos)
        {
            transform.DOKill();
            transform.DOMove(pos,1f).SetEase(Ease.Linear).OnComplete(() => _startPos = pos);
        }

        public CableBlock GetCableBlock()
        {
            return _cableBlock;
        }
    }
}