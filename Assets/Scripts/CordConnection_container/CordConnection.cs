using System;
using UnityEngine;

namespace WireConnection_container
{
    public class CordConnection : MonoBehaviour
    {
        [SerializeField] private Cord[] _cords;

        private Camera _camera;

        public void Initialize()
        {
            _camera = Camera.main;
            foreach (Cord cord in _cords)
            {
                cord.Initialize(_camera);
            }
        }
    }
}