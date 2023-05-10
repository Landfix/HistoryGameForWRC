using System;
using UI;
using UnityEngine;

namespace ComputerCleaning_container
{
    public class BootstrapperOfComputerCleaning : BaseBootstrapper
    {
        [SerializeField] private GameUi _gameUi;
        [SerializeField] private ComputerCleaning _computerCleaning;

        public override event Action Won;
        public override event Action Incorrected;
        
        private void Start()
        {
            _gameUi.Initialize(this);
            _computerCleaning.Initialize();
        }

        public override void ShowHint()
        {
            throw new System.NotImplementedException();
        }
    }
}