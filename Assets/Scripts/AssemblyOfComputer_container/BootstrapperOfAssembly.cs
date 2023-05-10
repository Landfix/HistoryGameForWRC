using System;
using UI;
using UnityEngine;

namespace AssemblyOfComputer_container
{
    public class BootstrapperOfAssembly : BaseBootstrapper
    {
        [SerializeField] private GameUi _gameUi;
        [SerializeField] AssemblyOfComputer _assemblyOfComputer;
    
        public override event Action Won;
        public override event Action Incorrected;
        
        private void Start()
        {
            _gameUi.Initialize(this);
            _assemblyOfComputer.Initialize();

            _assemblyOfComputer.AllConnected += Won;
            _assemblyOfComputer.Inconnected += Incorrected;
        }

        public override void ShowHint()
        {
            
        }
    }
}