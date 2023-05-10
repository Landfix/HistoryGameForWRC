using System;
using System.Linq;
using Pool_container;
using UnityEngine;

namespace ComputerCleaning_container
{
    public class ComputerCleaning : MonoBehaviour
    {
        [SerializeField] private Collider2D[] _colliders;
        [SerializeField] private CleansingMask _cleansingMaskPrefab;
        [SerializeField] private Transform _cleansingContainer;
        
        private IPool<CleansingMask> _cleansingMaskPool;
        private Camera _camera;

        public void Initialize()
        {
            _camera = Camera.main;
            _cleansingMaskPool = new Pool<CleansingMask>(_cleansingMaskPrefab, _cleansingContainer, 5, 7, true);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Collider2D collider = _colliders.FirstOrDefault(x => x.OverlapPoint(GetMousePosition()));

                if (collider) 
                    _cleansingMaskPool.GetElement(GetMousePosition());
            }

            if (Input.GetMouseButton(0))
            {
                Collider2D collider = _colliders.FirstOrDefault(x => x.OverlapPoint(GetMousePosition()));

                if (collider) 
                    _cleansingMaskPool.GetElement(GetMousePosition());
            }
        }

        public Vector2 GetMousePosition() => 
            _camera.ScreenToWorldPoint(Input.mousePosition);
    }
}