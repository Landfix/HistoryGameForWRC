using System;
using UI;
using UnityEngine;

namespace InputAndOutputDevice_container
{
    public class BootstrapperOfDevices : BaseBootstrapper
    {
        [SerializeField] private GameUi _gameUi;
        [SerializeField] private DeviceContainer _deviceContainer;
        
        public override event Action Won;
        public override event Action Incorrected;

        private void Start()
        {
            _gameUi.Initialize(this);
            _deviceContainer.Initialize();
        }

        public override void ShowHint()
        {
            throw new System.NotImplementedException();
        }
    }
}