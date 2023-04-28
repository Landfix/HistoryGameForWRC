using System;
using UnityEngine;

namespace ComputerCleaning_container
{
    public class Rubber : MonoBehaviour
    {
        private Camera _camera;
        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                transform.position = GetMousePosition();
            }

            if (Input.GetMouseButton(0))
            {
                transform.position = GetMousePosition();
            }
        }

        private Vector2 GetMousePosition()
        {
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            return mousePosition;
        }
    }
}